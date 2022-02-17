using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OllaInvoice.Api.ApiResponses;
using OllaInvoice.Api.AuthModels;
using OllaInvoice.Api.Services;
using OllaInvoice.Api.Utility;
using OllaInvoice.Entities.AuthEntities;
using System;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISendEmail _sendEmail;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
     
        public AuthController(UserManager<AppUser> userManager, ISendEmail sendEmail,
            SignInManager<AppUser> signInManager, ITokenService token)
        {
            _userManager = userManager;
            _sendEmail = sendEmail;
            _signInManager = signInManager;
            _token = token;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponses { Status = "Error", Message = "User already exists!" });

            AppUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string confirmationLink = Url.Action("ConfirmEmail", "Auth", new { user.Email, code = code }, protocol: HttpContext.Request.Scheme);
                string str = HelperMethods.GetTemplateString().Replace("#url", confirmationLink).Replace("#name", user.FirstName);
                await _sendEmail.SendConfirmationEmailAsync(user.Email, "Confirm your email", str);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new AuthResponses { Status = "Success", Message = "User created successfully!", Data = user });
            }
            else return StatusCode(StatusCodes.Status400BadRequest, new AuthResponses { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }





        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return StatusCode(StatusCodes.Status401Unauthorized, new AuthResponses { Status = "Error", Message = "Check credentials and try again" });
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return StatusCode(StatusCodes.Status401Unauthorized);
            return new LoginResponseDto
            {
                Email = user.Email,
                Username = user.UserName,
                Token = _token.CreateToken(user)
            };
        }


        


        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            if (email == null || code == null)
            {
                throw new Exception("Encountered an error");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Encountered an error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK, new AuthResponses { Status = "Success", Message = "User verified successfully" });
            }
            else return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponses { Status="Erroe", Message="Something went wrong"});
              
        }


    }
}
