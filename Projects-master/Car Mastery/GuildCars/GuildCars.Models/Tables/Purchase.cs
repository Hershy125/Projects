using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int VehicleListingId { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseTypeId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
