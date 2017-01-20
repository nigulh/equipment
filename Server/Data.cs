using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace Server
{
    public class Data
    {
        public static List<Equipment> Equipments = new List<Equipment>() 
        {
            new HeavyEquipment()
            {
                Name = "Caterpillar bulldozer",
                Url = "http://s7d2.scene7.com/is/image/Caterpillar/C10337180"
            },
            new RegularEquipment()
            {
                Name = "KamAZ truck",
                Url = "http://www.kamazexport.com/wp-content/uploads/2016/04/KAMAZ-65222-4-640x480.jpg"
            },
            new HeavyEquipment()
            {
                Name = "Komatsu crane",
                Url = "https://ae01.alicdn.com/kf/HTB1F9PuKFXXXXX6XVXXq6xXFXXXZ/JOAL-244-font-b-Komatsu-b-font-PC1100LC-6-with-font-b-Crane-b-font-Magnet.jpg"
            },
            new RegularEquipment()
            {
                Name = "Volvo steamroller",
                Url = "http://whyqatar.me/images/5244078576_d192ecc85c.jpg"
            },
            new SpecializedEquipment()
            {
                Name = "Bosch jackhammer",
                Url = "https://www.boschtools.com/us/en/ocsmedia/optimized/full/Bosch_Breaker_Hammer_BH2760VC_(EN).png"
            }
        };
    }
}
