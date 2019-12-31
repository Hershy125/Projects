using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class FeaturedVehicle
    {
        public int VehicleListingId { get; set; }
        public int Year { get; set; }
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public decimal SalePrice { get; set; }
        public string ImageFileName { get; set; }


    }
}
