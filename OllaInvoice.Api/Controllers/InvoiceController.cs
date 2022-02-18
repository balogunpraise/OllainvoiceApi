using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OllaInvoice.Api.Utility;
using OllaInvoice.Data;
using OllaInvoice.Entities;
using OllaInvoice.Entities.AuthEntities;
using OllaInvoice.Entities.Dtos;
using System;
using System.Linq;
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
        public async Task<IActionResult> AddAnInvoice(InvoiceRequestDto invoice)
        {
            
            var user = await  _userManager.GetUserAsync(HttpContext.User);
            try
            {
                Invoice newInvoice = new()
                {
                    CustomerName = invoice.CustomerName,
                    Tax = invoice.Tax,
                    Discount = invoice.Discount,
                    ImageUrl = invoice.ImageUrl,
                    BusinessName = invoice.BusinessName,
                    Account = invoice.Account,
                    Items = invoice.Items.Select(i => new Item
                    {
                        Description = i.Description,
                        PricePerUnit = i.PricePerUnit,
                        Units = i.Units
                    }).ToList(),
                    Number = Invoice.GetRandomNumber().ToString(),
                    CustomerEmail = invoice.CustomerEmail
                };
                await _invoiceRepository.AddInvoice(user, newInvoice);
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
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoice()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var invoices = await _invoiceRepository.GetAllInvoiceAsync(userId);
            return Ok(invoices);
        }


        [HttpGet("/invoice/{id}")]
        [Authorize]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
       
            if (user != null)
            {
                var fetchedInvoice = await _invoiceRepository.GetInvoiceById(id, user.Id.ToString());

                return AsDto.ReturnAsDto(fetchedInvoice);
            }
            return BadRequest("There is no invoice with the Id specified");
        }

    }
}