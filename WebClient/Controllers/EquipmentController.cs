using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using Shared;
using System.Threading.Tasks;
using Client.Service;

namespace Client.Controllers
{
    public class EquipmentController : Controller
    {
        IEndpointInstance endpoint;
        IEquipmentProvider dataProvider;

        public EquipmentController(IEndpointInstance endpoint, IEquipmentProvider dataProvider)
        {
            this.endpoint = endpoint;
            this.dataProvider = dataProvider;
        }

        // GET: /Equipments/
        [HttpGet]
        public async Task<ActionResult> List()
        {
            return View("List", await dataProvider.ListAll());
        }

        // GET: /Equipments/Rent/5
        public async Task<ActionResult> Rent(int id)
        {
            return View("Rent", await dataProvider.Get(id));
        }

        //
        // POST: /Equipments/Rent/5
        [HttpPost]
        public ActionResult Rent(int id, FormCollection collection)
        {
            try
            {
                var daysToRent = int.Parse(collection["daysToRent"]);
                dataProvider.Rent(id, daysToRent);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }


	}
}