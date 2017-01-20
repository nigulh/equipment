using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Collections.Generic;
using System;
using Server.Model;


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

            EquipmentList equipments = new EquipmentList();

            for (var i = 0; i < Data.Equipments.Count; i++)
            {
                var equipment = Data.Equipments[i];
                equipments.Add(new Shared.Equipment()
                {
                    Id = i,
                    Name = equipment.Name,
                    Url = equipment.Url,
                    Type = typeMap[equipment.GetType()]
                });
            }

            return context.Reply(equipments);
        }
    }
}