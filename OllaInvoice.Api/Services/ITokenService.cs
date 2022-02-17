using OllaInvoice.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
