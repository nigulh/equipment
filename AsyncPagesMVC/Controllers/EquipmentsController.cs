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

        public EquipmentsController(IEndpointInstance endpoint)
        {
            this.endpoint = endpoint;
        }

        // GET: /Equipments/
        [HttpGet]
        public async Task<ActionResult> List()
        {
            return View("List", await GetEquipmentList());
        }

        private EquipmentList _equipmentList = null;
        async Task<EquipmentList> GetEquipmentList()
        {
            if (_equipmentList == null)
            {
                _equipmentList = await Util.GetServerResponse<EquipmentList>(endpoint, new GetEquipmentList());
            }
            return _equipmentList;
        }


	}
}