using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OllaInvoice.Api.Services;
using OllaInvoice.Data;
using System;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace OllaInvoice.Api.Utility
{
    public class SendEmail : ISendEmail
    {
        private readonly IGeneratePdf _generatePdf;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEmailSender _emailSender;

        public SendEmail(IGeneratePdf generatePdf, IInvoiceRepository invoiceRepository, IEmailSender emailSender)
        {
            _generatePdf = generatePdf;
            _invoiceRepository = invoiceRepository;
            _emailSender = emailSender;
        }


        public async Task SendInvoiceAsAttachmentAsync(string emailAddress, int id)
        {
            string emailTitle = "Purchase Invoice";
            string emailBody = "Click the attachment file below to download your invoice";
            var result = await _invoiceRepository.GetCurrentInvoice(id);
            var pdfFile = await _generatePdf.GetPdf(@"~/Templates/template.cshtml", result);
            var formFileType = HelperMethods.ReturnFormFile((FileStreamResult)pdfFile);
            try
            {
                var message = new Message(new string[] { emailAddress }, emailTitle, emailBody, new IFormFile[] { formFileType });
                await _emailSender.SendMailAttachmentsAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SendConfirmationEmailAsync(string emailAddress, string emailTitle, string emailBody)
        {
            try
            {
                var message = new Message(new string[] { emailAddress }, emailTitle, emailBody);
                await _emailSender.SendMailAttachmentsAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        
        public async Task SendConfirmationEmail(string emailAddress, string emailBody)
        {
            string emailTitle = "Email Verification";
            try
            {
                var message = new Message(new string[] { emailAddress }, emailTitle, emailBody);
                await _emailSender.SendMailAttachmentsAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
