using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Client.Service;
using System.Threading.Tasks;
using Shared.Messages;

namespace Client.Controllers
{
    public class InvoiceController : Controller
    {
        IInvoiceService InvoiceService;

        public InvoiceController(IInvoiceService service)
        {
            this.InvoiceService = service;
        }

        // GET: /Invoice/
        public async Task<FileResult> Get()
        {
            var invoice = await InvoiceService.GetInvoice();
            byte[] fileBytes = Encoding.UTF8.GetBytes(String.Join("\r\n", GenerateFileData(invoice)));
            string fileName = string.Format(Resources.Invoice.Invoice_filename, invoice.Id);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public async Task<string> AsString()
        {
            var invoice = await InvoiceService.GetInvoice();
            return String.Join("\r\n", GenerateFileData(invoice));
        }
        
        public IEnumerable<string> GenerateFileData(InvoiceResponse invoice)
        {
            if (invoice.Items.Count == 0)
            {
                yield return Resources.Invoice.Empty_invoice;
                yield break;
            }
            yield return string.Format(Resources.Invoice.Invoice_title, invoice.Id);
            yield return "";
            foreach (var item in invoice.Items) {
                yield return string.Format(Resources.Invoice.Invoice_item_format, 
                    item.Name, 
                    string.Format(Resources.Invoice.Format_days,item.DaysToRent), 
                    string.Format(Resources.Invoice.Format_money, item.Price.ToString("#.##"), item.Currency.ToString()));
            }
            yield return "";
            yield return string.Format(Resources.Invoice.Total_price,
                string.Format(Resources.Invoice.Format_money,
                invoice.Items.Select(x => x.Price).Sum().ToString("#.##"), 
                invoice.Items[0].Currency));
            yield return string.Format(Resources.Invoice.Total_loyalty_points, invoice.Items.Select(x => x.LoyaltyPoints).Sum());
        }

    }
}