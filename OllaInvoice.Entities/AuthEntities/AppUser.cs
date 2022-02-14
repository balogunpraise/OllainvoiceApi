using Microsoft.AspNetCore.Identity;

namespace OllaInvoice.Entities.AuthEntities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
