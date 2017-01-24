using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using Shared;
using System.Threading.Tasks;
using AsyncPagesMVC.Service;

namespace AsyncPagesMVC.Controllers
{
    public class EquipmentsController : Controller
    {
        IEndpointInstance endpoint;
        IEquipmentProvider dataProvider;

        public EquipmentsController(IEndpointInstance endpoint, IEquipmentProvider dataProvider)
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


	}
}