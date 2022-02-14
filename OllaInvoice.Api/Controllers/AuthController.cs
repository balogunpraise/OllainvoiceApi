using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OllaInvoice.Api.ApiResponses;
using OllaInvoice.Api.AuthModels;
using OllaInvoice.Api.Utility;
using OllaInvoice.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IConfiguration _configuration;
        private readonly ISendEmail _sendEmail;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, 
            IConfiguration configuration, ISendEmail sendEmail, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _sendEmail = sendEmail;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponses { Status = "Error", Message = "User already exists!" });

            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string confirmationLink = Url.Action("ConfirmEmail", "Auth", new { user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                var confirm = new ConfirmEmailModel
                {
                    Username = user.FirstName,
                    Url = confirmationLink
                };
                await _sendEmail.SendConfirmationEmailAsync(user.Email, "Confirm your email",
                    /*$"Click the link to confirm you email {confirmationLink}"*/ System.IO.File.ReadAllText(@"C:\Users\David\Desktop\Project\OllaInvoice\OllaInvoice.Api\Templates\confirmemail.cshtml"));
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new AuthResponses { Status = "Success", Message = "User created successfully!", Data=user });
            }
            else return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponses { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }


        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new Exception("Encountered an error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Encountered an error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return RedirectToAction(result.Succeeded ? "Login" : "Error");
        }
    }
}
