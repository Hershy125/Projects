using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryReportVM
    {
        public IEnumerable<InventoryReportItem> NewInventoryReport { get; set; }
        public IEnumerable<InventoryReportItem> UsedInventoryReport { get; set; }

    }
}