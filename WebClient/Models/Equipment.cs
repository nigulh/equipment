using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Shared.Messages;

namespace Client.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType=typeof(Resources.Equipment))]
        public string Name { get; set; }

        [Display(Name="Type", ResourceType=typeof(Resources.Equipment))]
        public EquipmentType Type { get; set; }

        [Display(Name="Image", ResourceType=typeof(Resources.Equipment))]
        public string Url { get; set; }

        [Range(1, 100, ErrorMessage="InvalidDaysToRent", ErrorMessageResourceType=typeof(Resources.Equipment))]
        [Display(Name="DaysToRent", ResourceType=typeof(Resources.Equipment))]
        public int DaysToRent { get; set; }

        public static Client.Models.Equipment Convert(Shared.Messages.Equipment data)
        {
            return new Client.Models.Equipment()
            {
                Id = data.Id,
                Name = data.Name,
                Type = data.Type,
                Url = data.Url
            };
        }
    }
}