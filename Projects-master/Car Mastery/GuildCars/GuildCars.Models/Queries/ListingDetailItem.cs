using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class ListingDetailItem
    {
        public int VehicleListingId { get; set; }
        public int Year { get; set; }
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public int BodyStyleId { get; set; }
        public string BodyStyleName { get; set; }
        public int TransmissionTypeId { get; set; }
        public string TransmissionTypeName { get; set; }
        public decimal SalePrice { get; set; }
        public int Mileage { get; set; }
        public decimal MSRP { get; set; }
        public int InteriorColorId { get; set; }
        public int ColorId { get; set; }
        public string InteriorColor { get; set; }
        public string ColorName { get; set; }
        public string VIN { get; set; }
        public string ImageFileName { get; set; }
        public string Description { get; set; }
        public bool Sold { get; set; }

    }
}
