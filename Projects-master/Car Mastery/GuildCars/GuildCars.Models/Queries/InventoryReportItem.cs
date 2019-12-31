using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class InventoryReportItem
    {
        public int Year { get; set; }
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }
    }
}
