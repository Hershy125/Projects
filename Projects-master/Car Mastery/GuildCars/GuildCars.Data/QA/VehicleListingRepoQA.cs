using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class VehicleListingRepoQA : IVehicleListingRepository
    {
        public void Delete(int listingId)
        {
            _vehicleListings.RemoveAll(x => x.VehicleListingId == listingId);
        }

        public VehicleListing GetById(int listingId)
        {
            VehicleListing listing = null;

            listing = (from vehicleListing in _vehicleListings
                       where vehicleListing.VehicleListingId == listingId
                       select vehicleListing).FirstOrDefault();

            return listing;
        }

        public ListingDetailItem GetDetails(int listingId)
        {

            var bodyStyleRepo = new BodyStyleRepoQA();
            var colorRepo = new ColorRepoQA();
            var transmissionTypeRepo = new TransmissionTypeRepoQA();
            var makeRepo = new VehicleMakeRepoQA();
            var modelRepo = new VehicleModelRepoQA();
            var typeRepo = new VehicleTypeRepoQA();

            var listing = GetById(listingId);

            if(listing == null)
            {
                return null;
            }
            else
            {
                ListingDetailItem details = new ListingDetailItem();

                details.VehicleListingId = listing.VehicleListingId;
                details.Year = listing.Year;
                details.VehicleMakeId = listing.VehicleMakeId;
                details.VehicleMakeName = makeRepo.GetById(listing.VehicleMakeId).VehicleMakeName;
                details.VehicleModelId = listing.VehicleModelId;
                details.VehicleModelName = modelRepo.GetById(listing.VehicleModelId).VehicleModelName;
                details.BodyStyleId = listing.BodyStyleId;
                details.BodyStyleName = bodyStyleRepo.GetById(listing.BodyStyleId).BodyStyleName;
                details.TransmissionTypeId = listing.TransmissionTypeId;
                details.TransmissionTypeName = transmissionTypeRepo.GetById(listing.TransmissionTypeId).TransmissionTypeName;
                details.SalePrice = listing.SalePrice;
                details.Mileage = listing.Mileage;
                details.MSRP = listing.MSRP;
                details.InteriorColorId = listing.InteriorColorId;
                details.InteriorColor = colorRepo.GetById(listing.InteriorColorId).ColorName;
                details.ColorId = listing.ColorId;
                details.ColorName = colorRepo.GetById(listing.ColorId).ColorName;
                details.VIN = listing.VIN;
                details.ImageFileName = listing.ImageFileName;
                details.Description = listing.Description;


                return details;
            }

        }

        public IEnumerable<FeaturedVehicle> GetFeatured()
        {

            var makeRepo = new VehicleMakeRepoQA();
            var makes = makeRepo.GetVehicleMakes();
            var modelRepo = new VehicleModelRepoQA();
            var models = modelRepo.GetVehicleModels();

            var featuredVehicles = (from vehicleListing in _vehicleListings
                                   join make in makes
                                   on vehicleListing.VehicleMakeId equals make.VehicleMakeId
                                   join model in models
                                   on vehicleListing.VehicleModelId equals model.VehicleModelId
                                   where vehicleListing.FeaturedVehicle == true
                                   select new FeaturedVehicle
                                   {
                                        VehicleListingId = vehicleListing.VehicleListingId,
                                        Year = vehicleListing.Year,
                                        VehicleMakeId = vehicleListing.VehicleMakeId,
                                        VehicleMakeName = make.VehicleMakeName,
                                        VehicleModelId = vehicleListing.VehicleModelId,
                                        VehicleModelName = model.VehicleModelName,
                                        SalePrice = vehicleListing.SalePrice,
                                        ImageFileName = vehicleListing.ImageFileName
                                   }).Take(8);

            return featuredVehicles;
        }

        public void Insert(VehicleListing listing)
        {
            if(listing.VehicleListingId == 0)
            {
                var lastId = _vehicleListings.MaxBy(x => x.VehicleListingId).FirstOrDefault();
                int newId = lastId.VehicleListingId + 1;

                listing.VehicleListingId = newId;
            }

            _vehicleListings.Add(listing);
        }

        public void Update(VehicleListing listing)
        {
            _vehicleListings.RemoveAll(x => x.VehicleListingId == listing.VehicleListingId);

            VehicleListing newListing = new VehicleListing()
            {
                VehicleListingId = listing.VehicleListingId,
                VehicleMakeId = listing.VehicleMakeId,
                VehicleModelId = listing.VehicleModelId,
                VehicleTypeId = listing.VehicleTypeId,
                BodyStyleId = listing.BodyStyleId,
                TransmissionTypeId = listing.TransmissionTypeId,
                Year = listing.Year,
                ColorId = listing.ColorId,
                InteriorColorId = listing.InteriorColorId,
                Mileage = listing.Mileage,
                VIN = listing.VIN,
                MSRP = listing.MSRP,
                SalePrice = listing.SalePrice,
                FeaturedVehicle = listing.FeaturedVehicle,
                ImageFileName = listing.ImageFileName,
                Description = listing.Description,
                Sold = listing.Sold
            };

            _vehicleListings.Add(newListing);
        }

        public IEnumerable<ListingSearchItem> GetSearchResults(ListingSearchParameters parameters)
        {
            var listings = GetAll();
            var makesRepo = new VehicleMakeRepoQA();
            var modelsRepo = new VehicleModelRepoQA();
            var bodyRepo = new BodyStyleRepoQA();
            var transRepo = new TransmissionTypeRepoQA();
            var colorRepo = new ColorRepoQA();

            var makes = makesRepo.GetVehicleMakes();
            var models = modelsRepo.GetVehicleModels();
            var styles = bodyRepo.GetAll();
            var trans = transRepo.GetAll();
            var colors = colorRepo.GetAll();

            var query = from l in listings
                        join ma in makes on l.VehicleMakeId equals ma.VehicleMakeId
                        join mo in models on l.VehicleModelId equals mo.VehicleModelId
                        join s in styles on l.BodyStyleId equals s.BodyStyleId
                        join t in trans on l.TransmissionTypeId equals t.TransmissionTypeId
                        join c in colors on l.ColorId equals c.ColorId
                        join ic in colors on l.InteriorColorId equals ic.ColorId
                        where l.Sold == false
                        orderby l.MSRP descending
                        select new ListingSearchItem
                        {
                            VehicleListingId = l.VehicleListingId,
                            Year = l.Year,
                            VehicleMakeId = l.VehicleMakeId,
                            VehicleMakeName = ma.VehicleMakeName,
                            VehicleModelId = l.VehicleModelId,
                            VehicleModelName = mo.VehicleModelName,
                            BodyStyleId = l.BodyStyleId,
                            BodyStyleName = s.BodyStyleName,
                            TransmissionTypeId = l.TransmissionTypeId,
                            TransmissionTypeName = t.TransmissionTypeName,
                            SalePrice = l.SalePrice,
                            Mileage = l.Mileage,
                            MSRP = l.MSRP,
                            InteriorColorId = l.InteriorColorId,
                            InteriorColor = ic.ColorName,
                            ColorId = l.ColorId,
                            Color = c.ColorName,
                            VIN = l.VIN,
                            ImageFileName = l.ImageFileName,
                            VehicleTypeId = l.VehicleTypeId

                        };

            if (parameters.MinMSRP.HasValue)
            {
                query = query.Where(q => q.MSRP >= parameters.MinMSRP);
            }
            if (parameters.MaxMSRP.HasValue)
            {
                query = query.Where(q => q.MSRP <= parameters.MaxMSRP);
            }
            if (parameters.MinYear.HasValue)
            {
                query = query.Where(q => q.Year >= parameters.MinYear);
            }
            if (parameters.MaxYear.HasValue)
            {
                query = query.Where(q => q.Year <= parameters.MaxYear);
            }
            if (parameters.VehicleTypeId.HasValue)
            {
                query = query.Where(q => q.VehicleTypeId == parameters.VehicleTypeId);
            }
            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(q => q.VehicleMakeName.Contains(parameters.SearchTerm) || q.VehicleModelName.Contains(parameters.SearchTerm) || q.Year.ToString().Contains(parameters.SearchTerm));
            }


            return query;
        }

        public IEnumerable<VehicleListing> GetAll()
        {
            return _vehicleListings;
        }

        private static VehicleListing _testListing1 = new VehicleListing()
        {
            VehicleListingId = 1,
            VehicleMakeId = 2,
            VehicleModelId = 2,
            VehicleTypeId = 2,
            BodyStyleId = 2,
            TransmissionTypeId = 1,
            Year = 2011,
            ColorId = 3,
            InteriorColorId = 1,
            Mileage = 80000,
            VIN = "1HGBH41JXMN109186",
            MSRP = 18000.00M,
            SalePrice = 17800.00M,
            Description = "Fun and stylish Soul! Hamsters!",
            FeaturedVehicle = false,
            ImageFileName = "inventory-1.png",
            Sold = false

        };


        private static VehicleListing _testListing2 = new VehicleListing()
        {
            VehicleListingId = 2,
            VehicleMakeId = 1,
            VehicleModelId = 1,
            VehicleTypeId = 1,
            BodyStyleId = 5,
            TransmissionTypeId = 1,
            Year = 2019,
            ColorId = 2,
            InteriorColorId = 3,
            Mileage = 0,
            VIN = "2BXCH41JXMN109345",
            MSRP = 37000.00M,
            SalePrice = 35800.00M,
            Description = "Fun ford Explorer!",
            FeaturedVehicle = true,
            ImageFileName = "inventory-2.png",
            Sold = false

        };

        private static VehicleListing _testListing3 = new VehicleListing()
        {
            VehicleListingId = 3,
            VehicleMakeId = 1,
            VehicleModelId = 1,
            VehicleTypeId = 1,
            BodyStyleId = 2,
            TransmissionTypeId = 1,
            Year = 2018,
            ColorId = 1,
            InteriorColorId = 1,
            Mileage = 0,
            VIN = "2BXCH41JXMN109345",
            MSRP = 47000.00M,
            SalePrice = 45800.00M,
            Description = "Vroom",
            FeaturedVehicle = true,
            ImageFileName = "inventory-3.png",
            Sold = false

        };

        private static VehicleListing _testListing4 = new VehicleListing()
        {
            VehicleListingId = 4,
            VehicleMakeId = 2,
            VehicleModelId = 2,
            VehicleTypeId = 2,
            BodyStyleId = 4,
            TransmissionTypeId = 2,
            Year = 2014,
            ColorId = 3,
            InteriorColorId = 3,
            Mileage = 60000,
            VIN = "1HGBH41JXMN109186",
            MSRP = 28000.00M,
            SalePrice = 27800.00M,
            Description = "Test car",
            FeaturedVehicle = true,
            ImageFileName = "inventory-4.png",
            Sold = false

        };

        private static List<VehicleListing> _vehicleListings = new List<VehicleListing>()
        {
            _testListing1,
            _testListing2,
            _testListing3,
            _testListing4
        };


    }
}
