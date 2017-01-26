using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Messages;
using System.Collections.Generic;
using System;
using Server.Model;
using System.Linq;


namespace Server.Handler
{
    public class GetEquipmentListHandler :
        IHandleMessages<GetEquipmentList>
    {
        static ILog log = LogManager.GetLogger<GetEquipmentListHandler>();

        static Dictionary<Type, EquipmentType> typeMap = new Dictionary<Type, EquipmentType>() {
                {typeof(HeavyEquipment), EquipmentType.Heavy},
                {typeof(RegularEquipment), EquipmentType.Regular},
                {typeof(SpecializedEquipment), EquipmentType.Specialized}
            };

        public Task Handle(GetEquipmentList message, IMessageHandlerContext context)
        {
            log.Info("GetEquipmentList handling");

            var response = Data.Equipments.Select(ConvertEquipment);
            return context.Reply(new EquipmentList() { Items = response.ToList() });
        }

        private static Shared.Messages.Equipment ConvertEquipment(Model.Equipment equipment, int index)
        {
            return new Shared.Messages.Equipment()
            {
                Id = index,
                Name = equipment.Name,
                Url = equipment.Url,
                Type = typeMap[equipment.GetType()]
            };
        }
    }
}