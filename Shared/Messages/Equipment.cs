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
    
        public Equipment() { }
    }

    public class EquipmentList : List<Equipment>, IMessage
    {
        public EquipmentList() { }
    }

    public class GetEquipmentList : IMessage
    {

    }
}
