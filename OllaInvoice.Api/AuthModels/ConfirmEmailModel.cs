using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllaInvoice.Api.AuthModels
{
    public class ConfirmEmailModel
    {
        public string Username { get; set; }
        public string Url { get; set; }
    }
}
