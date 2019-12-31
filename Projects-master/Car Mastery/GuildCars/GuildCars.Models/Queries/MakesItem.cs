using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class MakesItem
    {
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

    }
}
