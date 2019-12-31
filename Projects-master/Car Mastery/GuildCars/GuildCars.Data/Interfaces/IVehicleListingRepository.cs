using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleListingRepository
    {
        VehicleListing GetById(int listingId);
        void Insert(VehicleListing listing);
        void Update(VehicleListing listing);
        void Delete(int listingId);
        IEnumerable<FeaturedVehicle> GetFeatured();
        ListingDetailItem GetDetails(int listingId);
        IEnumerable<ListingSearchItem> GetSearchResults(ListingSearchParameters parameters);
    }
}
