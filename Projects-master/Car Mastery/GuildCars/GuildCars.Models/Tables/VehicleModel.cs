using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class VehicleModel
    {
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleMakeId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
