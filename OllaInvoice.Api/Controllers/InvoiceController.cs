using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OllaInvoice.Api.Utility;
using OllaInvoice.Data;
using OllaInvoice.Entities;
using OllaInvoice.Entities.AuthEntities;
using OllaInvoice.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISendEmail _em;
        private readonly UserManager<AppUser> _userManager;

        public InvoiceController(IInvoiceRepository invoiceRepository, ISendEmail em, UserManager<AppUser> userManager)
        {
       
            _invoiceRepository = invoiceRepository;
            _em = em;
            _userManager = userManager;
        }

        [HttpPost("add-invoice")]
        [Authorize]
        public async Task<IActionResult> AddAnInvoice(InvoiceDto invoice)
        {
            //var clainms = User.Claims(User.FindFirst(c => c.Type == ))
            var user = await  _userManager.GetUserAsync(HttpContext.User);
            try
            {
                //var claims = User.Claims;
                //claims.First(c => c.Type == "").Value
                
                Invoice newInvoice = new()
                {
                    CustomerName = invoice.CustomerName,
                    Tax = invoice.Tax,
                    Discount = invoice.Discount,
                    ImageUrl = invoice.ImageUrl,
                    BusinessName = invoice.BusinessName,
                    Account = invoice.Account,
                    Items = invoice.Items,
                    Number = invoice.Number,
                    //AppUser = user,
                    CustomerEmail = invoice.CustomerEmail
                };
                await _invoiceRepository.AddInvoice(user, newInvoice);
                //user.Invoice.Add(newInvoice);
                await _em.SendInvoiceAsAttachmentAsync(newInvoice.CustomerEmail, newInvoice.Id);
                return Ok(newInvoice.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("/list-of-invoice")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoice()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var invoices = await _invoiceRepository.GetAllInvoiceAsync(userId);
            return Ok(invoices);
        }


        [HttpGet("/invoice{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
        {
            return Ok(await _invoiceRepository.GetInvoiceById(id));
        }

    }
}