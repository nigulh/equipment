using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;


namespace Server
{
    public class GetEquipmentListHandler :
        IHandleMessages<GetEquipmentList>
    {
        static ILog log = LogManager.GetLogger<GetEquipmentListHandler>();

        public Task Handle(GetEquipmentList message, IMessageHandlerContext context)
        {
            log.Info("GetEquipmentList handling");

            EquipmentList equipments = new EquipmentList();

            equipments.Add(new Equipment() { 
                Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy, 
                Id = 0, Url = "http://s7d2.scene7.com/is/image/Caterpillar/C10337180" });


            return context.Reply(equipments);
        }
    }
}