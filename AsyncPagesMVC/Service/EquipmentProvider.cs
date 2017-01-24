using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared;
using System.Threading.Tasks;
using NServiceBus;

namespace AsyncPagesMVC.Service
{
    public interface IEquipmentProvider
    {
        Task<EquipmentList> ListAll();
    }

    public class EquipmentProvider : IEquipmentProvider
    {
        private EquipmentList _equipmentList = null;
        IEndpointInstance endpoint;

        public EquipmentProvider(IEndpointInstance endpoint)
        {
            this.endpoint = endpoint;
        }

        public async Task<EquipmentList> ListAll()
        {
            // TODO: Make it thread safe
            if (_equipmentList == null)
            {
                _equipmentList = await Util.GetServerResponse<EquipmentList>(endpoint, new GetEquipmentList());
            }
            return _equipmentList;
        }
    }
}