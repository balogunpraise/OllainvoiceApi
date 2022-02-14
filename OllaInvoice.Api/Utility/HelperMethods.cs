using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace OllaInvoice.Api.Utility
{
    public static class HelperMethods
    {

        
        public static IFormFile ReturnFormFile(FileStreamResult result)
        {
            var ms = new MemoryStream();
            try
            {
                result.FileStream.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "name", "Invoice");
            }
            catch (Exception)
            {
                //ms.Dispose();
                throw;
            }
            finally
            {
                //ms.Dispose();
            }
        }

        public static IFormFile ReturnFormFile(byte[] result)
        {
            try
            {
                var stream = new MemoryStream(result);
                
                return new FormFile(stream, 0, stream.Length, "name", "Invoice");
            }
            catch (Exception)
            {
                //ms.Dispose();
                throw;
            }
            finally
            {
                //ms.Dispose();
            }
        }
    }
  
}
