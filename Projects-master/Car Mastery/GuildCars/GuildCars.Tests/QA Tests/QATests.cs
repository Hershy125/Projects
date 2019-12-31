using GuildCars.Data.QA;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.QA_Tests
{
    [TestFixture]
    public class QATests
    {
        [SetUp]
        public void Init()
        {
            var repo = new VehicleListingRepoQA();

            if (repo.GetById(2) != null)
                repo.Delete(2);
        }


        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepoQA();
            var bodyStyles = repo.GetAll();

            Assert.AreEqual(5, bodyStyles.Count());

            Assert.AreEqual("SUV", bodyStyles[4].BodyStyleName);
            Assert.AreEqual(2, bodyStyles[1].BodyStyleId);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepoQA();
            var colors = repo.GetAll();

            Assert.AreEqual(4, colors.Count());
            Assert.AreEqual("Blue", colors[2].ColorName);
            Assert.AreEqual(1, colors[0].ColorId);
            
        }

        [Test]
        public void CanLoadContactRequests()
        {
            var repo = new ContactRequestRepoQA();
            var requests = repo.GetAll();

            Assert.AreEqual(2, requests.Count());
            Assert.AreEqual(1, requests[0].ContactRequestId);
            Assert.AreEqual("test@test.com", requests[0].Email);
            Assert.AreEqual("Testy McTesterson", requests[0].Name);
            Assert.AreEqual("123-456-7890", requests[0].Phone);
            Assert.AreEqual("Test contact request", requests[0].Message);

        }

        [Test]
        public void CanAddContactRequest()
        {
            var repo = new ContactRequestRepoQA();

            ContactRequest newRequest = new ContactRequest()
            {
                Name = "John",
                Email = "John@test.com",
                Phone = "222-222-2222",
                Message = "Hi this is john"
            };

            repo.Insert(newRequest);

            var requests = repo.GetAll();

            Assert.AreEqual(2, requests.Count());
            Assert.AreEqual(2, requests[1].ContactRequestId);
            Assert.AreEqual("John", requests[1].Name);
            Assert.AreEqual("John@test.com", requests[1].Email);
            Assert.AreEqual("222-222-2222", requests[1].Phone);
            Assert.AreEqual("Hi this is john", requests[1].Message);
        }

        [Test]
        public void CanLoadCustomers()
        {
            var repo = new CustomerRepoQA();
            var customers = repo.GetAll();

            Assert.AreEqual(2, customers.Count());
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.AreEqual("Tester One", customers[0].Name);
            Assert.AreEqual("122-222-3344", customers[0].Phone);
            Assert.AreEqual("testing@tests.com", customers[0].Email);
            Assert.AreEqual("123 state street", customers[0].Street1);
            Assert.AreEqual("Apt 101", customers[0].Street2);
            Assert.AreEqual("Newark", customers[0].City);
            Assert.AreEqual("NJ", customers[0].State);
            Assert.AreEqual(07992, customers[0].ZipCode);
        }

        [Test]
        public void CanAddCustomer()
        {
            var repo = new CustomerRepoQA();

            Customer newCustomer = new Customer()
            {
                Name = "Tester Two",
                Phone = "111-111-1111",
                Email = "test2@tests.com",
                Street1 = "456 state street",
                Street2 = "Unit 3",
                City = "Bergen",
                State = "KS",
                ZipCode = 43210
            };

            repo.Insert(newCustomer);

            var customers = repo.GetAll();

            Assert.AreEqual(2, customers.Count());
            Assert.AreEqual(2, customers[1].CustomerId);
            Assert.AreEqual("Tester Two", customers[1].Name);
            Assert.AreEqual("111-111-1111", customers[1].Phone);
            Assert.AreEqual("test2@tests.com", customers[1].Email);
            Assert.AreEqual("456 state street", customers[1].Street1);
            Assert.AreEqual("Unit 3", customers[1].Street2);
            Assert.AreEqual("Bergen", customers[1].City);
            Assert.AreEqual("KS", customers[1].State);
            Assert.AreEqual(43210, customers[1].ZipCode);
        }

        [Test]
        public void CanLoadVehicleListing()
        {
            var repo = new VehicleListingRepoQA();

            var listing = repo.GetById(1);

            Assert.AreEqual(2, listing.VehicleMakeId);
            Assert.AreEqual(2, listing.VehicleModelId);
            Assert.AreEqual(2, listing.VehicleTypeId);
            Assert.AreEqual(2, listing.BodyStyleId);
            Assert.AreEqual(1, listing.TransmissionTypeId);
            Assert.AreEqual(2011, listing.Year);
            Assert.AreEqual(3, listing.ColorId);
            Assert.AreEqual(1, listing.InteriorColorId);
            Assert.AreEqual(80000, listing.Mileage);
            Assert.AreEqual("1HGBH41JXMN109186", listing.VIN);
            Assert.AreEqual(17800.00M, listing.SalePrice);
            Assert.AreEqual(18000M, listing.MSRP);
            Assert.AreEqual("Fun and stylish Soul! Hamsters!", listing.Description);
            Assert.AreEqual(false, listing.FeaturedVehicle);
            Assert.AreEqual(false, listing.Sold);
            Assert.AreEqual("Placeholder.png", listing.ImageFileName);

        }

        [Test]
        public void CanLoadVehicleDetails()
        {
            var repo = new VehicleListingRepoQA();

            var listing = repo.GetDetails(1);

            Assert.AreEqual(2, listing.VehicleMakeId);
            Assert.AreEqual("Kia", listing.VehicleMakeName);
            Assert.AreEqual(2, listing.VehicleModelId);
            Assert.AreEqual("Soul", listing.VehicleModelName);
            Assert.AreEqual(2, listing.BodyStyleId);
            Assert.AreEqual("Hatchback", listing.BodyStyleName);
            Assert.AreEqual(1, listing.TransmissionTypeId);
            Assert.AreEqual("Automatic", listing.TransmissionTypeName);
            Assert.AreEqual(2011, listing.Year);
            Assert.AreEqual(3, listing.ColorId);
            Assert.AreEqual("Blue", listing.ColorName);
            Assert.AreEqual(1, listing.InteriorColorId);
            Assert.AreEqual("Black", listing.InteriorColor);
            Assert.AreEqual(80000, listing.Mileage);
            Assert.AreEqual("1HGBH41JXMN109186", listing.VIN);
            Assert.AreEqual(17800.00M, listing.SalePrice);
            Assert.AreEqual(18000M, listing.MSRP);
            Assert.AreEqual("Fun and stylish Soul! Hamsters!", listing.Description);
            Assert.AreEqual("Placeholder.png", listing.ImageFileName);
        }

        [Test]
        public void NotFoundListingReturnsNull()
        {
            var repo = new VehicleListingRepoQA();

            var listing = repo.GetById(1000);

            Assert.IsNull(listing);

            var listingDetails = repo.GetDetails(1000);

            Assert.IsNull(listingDetails);
        }

        [Test]
        public void CanInsertListing()
        {
            var repo = new VehicleListingRepoQA();

            VehicleListing car = new VehicleListing()
            {
                VehicleMakeId = 1,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 5,
                TransmissionTypeId = 1,
                Year = 1997,
                ColorId = 2,
                InteriorColorId = 4,
                Mileage = 20000,
                VIN = "11111111111111111",
                MSRP = 40000.00M,
                SalePrice = 36000.00M,
                Description = "Ultimate sport utility vehicle",
                FeaturedVehicle = true,
                Sold = false,
                ImageFileName = "placeholder.png"

            };

            repo.Insert(car);

            var addedListing = repo.GetById(5);

            Assert.AreEqual(5, addedListing.VehicleListingId);
            Assert.AreEqual(1, addedListing.VehicleMakeId);

            var featuredListings = repo.GetFeatured().ToList();

            Assert.AreEqual(3, featuredListings.Count());
            Assert.AreEqual(5, featuredListings[2].VehicleListingId);
            Assert.AreEqual(1997, featuredListings[2].Year);
            Assert.AreEqual("Ford", featuredListings[2].VehicleMakeName);
            Assert.AreEqual("Explorer", featuredListings[2].VehicleModelName);
        }

        [Test]
        public void CanUpdateListing()
        {
            var repo = new VehicleListingRepoQA();

            VehicleListing car = new VehicleListing()
            {
                VehicleMakeId = 2,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 3,
                TransmissionTypeId = 1,
                Year = 1997,
                ColorId = 2,
                InteriorColorId = 4,
                Mileage = 20000,
                VIN = "11111111111111111",
                MSRP = 40000.00M,
                SalePrice = 36000.00M,
                Description = "Ultimate",
                FeaturedVehicle = true,
                Sold = false,
                ImageFileName = "placeholder.png"

            };

            repo.Insert(car);
            var listingToUpdate = repo.GetById(5);

            listingToUpdate.TransmissionTypeId = 2;
            listingToUpdate.ColorId = 3;
            listingToUpdate.InteriorColorId = 1;
            listingToUpdate.Mileage = 1000;
            listingToUpdate.MSRP = 60000.00M;
            listingToUpdate.SalePrice = 48000.00M;
            listingToUpdate.Description = "Updated sport utility vehicle";

            repo.Update(listingToUpdate);

            var updatedListing = repo.GetById(5);

            Assert.AreEqual(2, updatedListing.TransmissionTypeId);
            Assert.AreEqual(3, updatedListing.ColorId);
            Assert.AreEqual(1, updatedListing.InteriorColorId);
            Assert.AreEqual(1000, updatedListing.Mileage);
            Assert.AreEqual(48000.00M, updatedListing.SalePrice);
            Assert.AreEqual(60000.00M, updatedListing.MSRP);
            Assert.AreEqual("Updated sport utility vehicle", updatedListing.Description);
            Assert.AreEqual(true, updatedListing.FeaturedVehicle);
            Assert.AreEqual("11111111111111111", updatedListing.VIN);

        }

        [Test]
        public void CanDeleteListing()
        {
            var repo = new VehicleListingRepoQA();

            VehicleListing car = new VehicleListing()
            {
                VehicleMakeId = 2,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 3,
                TransmissionTypeId = 1,
                Year = 1997,
                ColorId = 2,
                InteriorColorId = 4,
                Mileage = 20000,
                VIN = "11111111111111111",
                MSRP = 40000.00M,
                SalePrice = 36000.00M,
                Description = "Ultimate",
                FeaturedVehicle = true,
                Sold = false,
                ImageFileName = "placeholder.png"

            };

            repo.Insert(car);

            var listing = repo.GetById(5);
            Assert.IsNotNull(listing);

            repo.Delete(5);

            listing = repo.GetById(5);

            Assert.IsNull(listing);
        }

        [Test]
        public void CanGetSearchResults()
        {
            var listingRepo = new VehicleListingRepoQA();
            var parameters = new ListingSearchParameters()
            {
                VehicleTypeId = 1
            };

            var searchResults = listingRepo.GetSearchResults(parameters);

            Assert.AreEqual(1, searchResults.Count());

        }

        [Test]
        public void CanInsertPurchase()
        {
            var repo = new PurchaseRepoQA();

            Purchase model = new Purchase()
            {
                UserId = "4fc0bbac-78c6-4092-b9c6-b7d726a5a51b",
                VehicleListingId = 4,
                CreatedDate = DateTime.Now,
                CustomerId = 1,
                PurchasePrice = 35000,
                PurchaseTypeId = 2

            };

            repo.Insert(model);

            var purchases = repo.GetAll();

            Assert.AreEqual(4, purchases.Count());
            Assert.AreEqual(4, purchases[3].VehicleListingId);
            Assert.AreEqual(35000, purchases[3].PurchasePrice);
            Assert.AreEqual(4, purchases[3].PurchaseId);
            Assert.AreEqual(2, purchases[3].PurchaseTypeId);

        }
    }
}
