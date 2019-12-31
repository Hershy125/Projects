using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class VehicleMake
    {
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
