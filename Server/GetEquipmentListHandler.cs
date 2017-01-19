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

            equipments.Add(new Equipment()
            {
                Name = "Caterpillar bulldozer",
                Type = EquipmentType.Heavy,
                Id = 0,
                Url = "http://s7d2.scene7.com/is/image/Caterpillar/C10337180"
            });
            equipments.Add(new Equipment()
            {
                Name = "KamAZ truck",
                Type = EquipmentType.Regular,
                Id = 1,
                Url = "http://www.kamazexport.com/wp-content/uploads/2016/04/KAMAZ-65222-4-640x480.jpg"
            });

            return context.Reply(equipments);
        }
    }
}