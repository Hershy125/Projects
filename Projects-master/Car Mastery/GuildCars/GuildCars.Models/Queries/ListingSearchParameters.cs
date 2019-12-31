using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class ListingSearchParameters
    {
        public string SearchTerm { get; set; }
        public decimal? MinMSRP { get; set; }
        public decimal? MaxMSRP { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? VehicleTypeId { get; set; }

    }
}
