using System.ComponentModel.DataAnnotations;

namespace OllaInvoice.Api.AuthModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
