using System.ComponentModel.DataAnnotations;

namespace OllaInvoice.Api.AuthModels
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
