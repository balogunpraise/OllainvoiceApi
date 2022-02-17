using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OllaInvoice.Entities.AuthEntities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Invoice> Invoice { get; set; } = new List<Invoice>();
    }
}
