using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;
using System.Threading.Tasks;
using NServiceBus;
using Shared.Messages;

namespace Client.Service
{
    public interface IEquipmentProvider
    {
        Task<EquipmentList> ListAll();
        Task<Client.Models.Equipment> Get(int id);
        void Rent(int id, int days);
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
            await EnsureEquipmentList();
            return _equipmentList;
        }

        private async Task EnsureEquipmentList()
        {
            // TODO: Make it thread safe
            if (_equipmentList == null)
            {
                _equipmentList = await Util.GetServerResponse<EquipmentList>(endpoint, new GetEquipmentList());
            }
        }

        public async Task<Client.Models.Equipment> Get(int id)
        {
            await EnsureEquipmentList();
            return Client.Models.Equipment.Convert(_equipmentList.Items[id]);
        }

        public async void Rent(int id, int days)
        {
            var request = new RentRequest() { ItemId = id, DaysToRent = days };
            var response = await Util.GetServerResponse<RentResponse>(endpoint, request);
        }
    }
}