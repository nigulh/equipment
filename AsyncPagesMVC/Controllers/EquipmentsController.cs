using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using Shared;
using System.Threading.Tasks;

namespace AsyncPagesMVC.Controllers
{
    public class EquipmentsController : Controller
    {
        IEndpointInstance endpoint;

        public EquipmentsController(IEndpointInstance endpoint)
        {
            this.endpoint = endpoint;
        }

        // GET: /Equipments/
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var command = new GetEquipmentList();

            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Samples.Mvc.Server");
            var response = await endpoint.Request<Equipment>(command, sendOptions).ConfigureAwait(false);

            return View("List", response);
        }
	}
}