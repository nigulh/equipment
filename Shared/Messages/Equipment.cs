using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Shared
{
    public enum EquipmentType 
    {
        Heavy, Regular, Specialized
    }

    [Serializable]
    public class Equipment : IMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public string Url { get; set; }
        public List<GetEquipmentList> specials { get; set; }
    
        public Equipment() 
        {
            specials = new List<GetEquipmentList>();
        }
    }

    public class EquipmentList : IMessage
    {
        public List<Equipment> Items { get; set; }
        public EquipmentList() 
        {
            Items = new List<Equipment>();
        }

        public void Add(Equipment item)
        {
            Items.Add(item);
        }
    }

    public class GetEquipmentList : IMessage
    {

    }
}
