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
    public interface IEquipmentService
    {
        Task<EquipmentList> ListAll();
        Task<Client.Models.Equipment> Get(int id);
        void Rent(int id, int days);
    }

    public class EquipmentService : IEquipmentService
    {
        private EquipmentList _equipmentList = null;
        IServerMessageService server;

        public EquipmentService(IServerMessageService server)
        {
            this.server = server;
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
                _equipmentList = await server.GetResponse<EquipmentList>(new GetEquipmentList());
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
            var response = await server.GetResponse<RentResponse>(request);
        }
    }
}