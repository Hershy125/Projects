using GuildCars.Data.Production;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GuildCars.Models.Queries;

namespace GuildCars.Tests.Integration_Tests
{
    [TestFixture]
    public class DapperTests
    {

        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        [Test]
        public void CanLoadVehicleMakes()
        {
            var repo = new VehicleMakeRepository();

            var makes = repo.GetVehicleMakes().ToList();

            Assert.AreEqual(5, makes.Count());
            Assert.AreEqual("Ford", makes[0].VehicleMakeName);
            Assert.AreEqual(1, makes[0].VehicleMakeId);

        }

        [Test]
        public void CanLoadVehicleModels()
        {
            var repo = new VehicleModelRepository();

            var models = repo.GetVehicleModels().ToList();

            Assert.AreEqual(5, models.Count());
            Assert.AreEqual("Explorer", models[0].VehicleModelName);
            Assert.AreEqual(1, models[0].VehicleMakeId);
            Assert.AreEqual("Ram 1500", models[3].VehicleModelName);
            Assert.AreEqual(2, models[1].VehicleMakeId);
        }

        [Test]
        public void CanLoadMakeById()
        {
            var repo = new VehicleMakeRepository();

            var make = repo.GetById(2);

            Assert.AreEqual("Kia", make.VehicleMakeName);
        }

        [Test]
        public void CanLoadModelById()
        {
            var repo = new VehicleModelRepository();

            var model = repo.GetById(2);

            Assert.AreEqual("Soul", model.VehicleModelName);
        }

        [Test]
        public void CanInsertVehicleMake()
        {
            var repo = new VehicleMakeRepository();

            MakesItem newMake = new MakesItem()
            {
                VehicleMakeName = "Subaru",
                UserId = "00000000-0000-0000-0000-000000000000"
            };

            repo.Insert(newMake);

            var allMakes = repo.GetVehicleMakes().ToList();

            Assert.AreEqual(6, allMakes.Count());
            Assert.AreEqual("Subaru", allMakes[5].VehicleMakeName);

            
        }

        [Test]
        public void CanInsertVehicleModel()
        {
            var repo = new VehicleModelRepository();

            ModelsItem newModel = new ModelsItem()
            {
                VehicleModelName = "Matrix",
                VehicleMakeId = 5,
                UserId = "00000000-0000-0000-0000-000000000000"
            };

            repo.Insert(newModel);

            var allModels = repo.GetVehicleModels().ToList();

            Assert.AreEqual(6, allModels.Count());
            Assert.AreEqual("Matrix", allModels[5].VehicleModelName);



        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepository();

            var styles = repo.GetAll();

            Assert.AreEqual(5, styles.Count());
            Assert.AreEqual("Convertible", styles[0].BodyStyleName);
            Assert.AreEqual(3, styles[2].BodyStyleId);
            Assert.AreEqual("SUV", styles[4].BodyStyleName);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepository();

            var colors = repo.GetAll();

            Assert.AreEqual(5, colors.Count());
            Assert.AreEqual("Black", colors[0].ColorName);
            Assert.AreEqual(3, colors[2].ColorId);
            Assert.AreEqual("Gray", colors[4].ColorName);
        }


        [Test]
        public void CanLoadTransmissionTypes()
        {
            var repo = new TransmissionTypeRepository();

            var transmissions = repo.GetAll();

            Assert.AreEqual(2, transmissions.Count());
            Assert.AreEqual("Automatic", transmissions[0].TransmissionTypeName);
            Assert.AreEqual("Manual", transmissions[1].TransmissionTypeName);

        }

        [Test]
        public void CanLoadVehicleTypes()
        {
            var repo = new VehicleTypeRepository();

            var types = repo.GetAll();

            Assert.AreEqual(2, types.Count());
            Assert.AreEqual("New", types[0].VehicleTypeName);
            Assert.AreEqual("Used", types[1].VehicleTypeName);

        }

        [Test]
        public void CanInsertContactRequest()
        {
            var repo = new ContactRequestRepository();

            ContactRequest request = new ContactRequest()
            {
                Name = "John Smith",
                Email = "test@test.com",
                Phone = "222-222-2222",
                Message = "Hi! this is a test!"
            };

            repo.Insert(request);

            var requests = repo.GetAll();

            
            
            Assert.AreEqual("John Smith", requests[1].Name);
            Assert.AreEqual("test@test.com", requests[1].Email);
            Assert.AreEqual("222-222-2222", requests[1].Phone);
            Assert.AreEqual("Hi! this is a test!", requests[1].Message);


        }

        [Test]
        public void CanLoadCustomers()
        {
            var repo = new CustomerRepository();

            var customers = repo.GetAll();

            Assert.AreEqual(1, customers.Count());
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.AreEqual("Test Buyer", customers[0].Name);
            Assert.AreEqual("111-111-1111", customers[0].Phone);
            Assert.AreEqual("tester@test.com", customers[0].Email);
            Assert.AreEqual("123 state street", customers[0].Street1);
            Assert.AreEqual("apt 2", customers[0].Street2);
            Assert.AreEqual("Union", customers[0].City);
            Assert.AreEqual("NJ", customers[0].State);
            Assert.AreEqual(07930, customers[0].ZipCode);


        }

        [Test]
        public void CanInsertCustomer()
        {
            var repo = new CustomerRepository();

            Customer test = new Customer()
            {
                Name = "Jon Smith",
                Phone = "222-222-2222",
                Email = "Jon@test.com",
                Street1 = "222 2nd st",
                Street2 = "Unit 1",
                City = "Long Beach",
                State = "CA",
                ZipCode = 90720
            };

            repo.Insert(test);


            var customers = repo.GetAll();

            
            
            Assert.AreEqual("Jon Smith", customers[1].Name);
            Assert.AreEqual("222-222-2222", customers[1].Phone);
            Assert.AreEqual("Jon@test.com", customers[1].Email);
            Assert.AreEqual("222 2nd st", customers[1].Street1);
            Assert.AreEqual("Unit 1", customers[1].Street2);
            Assert.AreEqual("Long Beach", customers[1].City);
            Assert.AreEqual("CA", customers[1].State);
            Assert.AreEqual(90720, customers[1].ZipCode);


        }

        [Test]
        public void CanLoadPurchases()
        {
            var repo = new PurchaseRepository();

            var purchases = repo.GetAll();

            Assert.AreEqual(2, purchases.Count());
            Assert.AreEqual(1, purchases[0].PurchaseId);
            Assert.AreEqual(4, purchases[0].VehicleListingId);
            Assert.AreEqual(1, purchases[0].CustomerId);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", purchases[0].UserId);
            Assert.AreEqual(45500M, purchases[0].PurchasePrice);
            Assert.AreEqual(2, purchases[0].PurchaseTypeId);

            var purchasesByUser = repo.GetByUserId("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual(2, purchases.Count());
            Assert.AreEqual(2, purchases[1].PurchaseId);
            Assert.AreEqual(3, purchases[1].VehicleListingId);
            Assert.AreEqual(1, purchases[1].CustomerId);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", purchases[1].UserId);
            Assert.AreEqual(25500M, purchases[1].PurchasePrice);
            Assert.AreEqual(1, purchases[1].PurchaseTypeId);

        }

        [Test]
        public void CanInsertPurchase()
        {
            var repo = new PurchaseRepository();

            Purchase test = new Purchase()
            {
                VehicleListingId = 1,
                CustomerId = 1,
                UserId = "00000000-0000-0000-0000-000000000000",
                PurchasePrice = 72000M,
                PurchaseTypeId = 3
   
            };

            repo.Insert(test);


            var purchases = repo.GetAll();


            Assert.AreEqual(3, purchases.Count());
            Assert.AreEqual(1, purchases[2].VehicleListingId);
            Assert.AreEqual(1, purchases[2].CustomerId);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", purchases[2].UserId);
            Assert.AreEqual(72000M, purchases[2].PurchasePrice);
            Assert.AreEqual(3, purchases[2].PurchaseTypeId);

        }


        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepository();

            var types = repo.GetAll();

            Assert.AreEqual(3, types.Count());
            Assert.AreEqual("Dealer Financing", types[0].PurchaseTypeName);
            Assert.AreEqual("Bank Financing", types[1].PurchaseTypeName);
            Assert.AreEqual("Cash", types[2].PurchaseTypeName);

        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialRepository();

            var specials = repo.GetAll();

            Assert.AreEqual(2, specials.Count());
            Assert.AreEqual(1, specials[0].SpecialId);
            Assert.AreEqual(2, specials[1].SpecialId);
            Assert.AreEqual("A Great Special", specials[0].SpecialTitle);
            Assert.AreEqual("A pretty good special", specials[1].SpecialTitle);
            Assert.AreEqual("this special is really great", specials[0].SpecialDetails);
            Assert.AreEqual("this special is pretty good", specials[1].SpecialDetails);

        }

        [Test]
        public void CanInsertSpecial()
        {
            var repo = new SpecialRepository();

            Special test = new Special()
            {
                SpecialTitle = "Testing!",
                SpecialDetails = "test test",

            };

            repo.Insert(test);


            var specials = repo.GetAll();


            Assert.AreEqual(3, specials.Count());
            Assert.AreEqual(3, specials[2].SpecialId);
            Assert.AreEqual("Testing!", specials[2].SpecialTitle);
            Assert.AreEqual("test test", specials[2].SpecialDetails);

        }

        [Test]
        public void CanLoadListingById()
        {
            var repo = new VehicleListingRepository();

            var vehicle = repo.GetById(2);

            Assert.AreEqual(2, vehicle.VehicleMakeId);
            Assert.AreEqual(2, vehicle.VehicleModelId);
            Assert.AreEqual(1, vehicle.VehicleTypeId);
            Assert.AreEqual(19000M, vehicle.MSRP);
            Assert.AreEqual(16500M, vehicle.SalePrice);
            Assert.AreEqual("test car 2", vehicle.Description);
       

        }

        [Test]
        public void CanLoadVehicleDetails()
        {
            var repo = new VehicleListingRepository();

            var vehicle = repo.GetDetails(2);

            Assert.AreEqual("Kia", vehicle.VehicleMakeName);
            Assert.AreEqual("Soul", vehicle.VehicleModelName);
            Assert.AreEqual("SUV", vehicle.BodyStyleName);
            Assert.AreEqual("Automatic", vehicle.TransmissionTypeName);
            Assert.AreEqual("Blue", vehicle.ColorName);
            Assert.AreEqual("Beige", vehicle.InteriorColor);
            Assert.AreEqual(1000, vehicle.Mileage);



        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {
            var repo = new VehicleListingRepository();

            var vehicles = repo.GetFeatured().ToList();

            Assert.AreEqual(8, vehicles.Count());
            Assert.AreEqual(1, vehicles[0].VehicleListingId);
            Assert.AreEqual("Ford", vehicles[0].VehicleMakeName);
            Assert.AreEqual("Explorer", vehicles[0].VehicleModelName);

        }

        [Test]
        public void CanInsertVehicleListing()
        {
            var repo = new VehicleListingRepository();

            VehicleListing test = new VehicleListing()
            {
                VehicleMakeId = 1,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 3,
                TransmissionTypeId = 2,
                Year = 2020,
                ColorId = 3,
                InteriorColorId = 1,
                Mileage = 0,
                VIN = "test",
                MSRP = 99000M,
                SalePrice = 92000M,
                Description = "New Car",
                FeaturedVehicle = true,
                ImageFileName = "test.png",
                Sold = false


            };

            repo.Insert(test);


            var vehicle = repo.GetById(9);


            Assert.AreEqual(1, vehicle.VehicleMakeId);
            Assert.AreEqual(1, vehicle.VehicleModelId);
            Assert.AreEqual(2, vehicle.VehicleTypeId);
            Assert.AreEqual(3, vehicle.BodyStyleId);
            Assert.AreEqual(2, vehicle.TransmissionTypeId);
            Assert.AreEqual(2020, vehicle.Year);
            Assert.AreEqual(3, vehicle.ColorId);
            Assert.AreEqual(1, vehicle.InteriorColorId);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual("test", vehicle.VIN);
            Assert.AreEqual(99000M, vehicle.MSRP);
            Assert.AreEqual(92000M, vehicle.SalePrice);
            Assert.AreEqual("New Car", vehicle.Description);
            Assert.AreEqual(true, vehicle.FeaturedVehicle);
            Assert.AreEqual("test.png", vehicle.ImageFileName);
            Assert.AreEqual(false, vehicle.Sold);

        }

        [Test]
        public void CanUpdateVehicleListing()
        {
            var repo = new VehicleListingRepository();

            VehicleListing test = new VehicleListing()
            {
                VehicleMakeId = 1,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 3,
                TransmissionTypeId = 2,
                Year = 2020,
                ColorId = 3,
                InteriorColorId = 1,
                Mileage = 0,
                VIN = "test",
                MSRP = 99000M,
                SalePrice = 92000M,
                Description = "New Car",
                FeaturedVehicle = true,
                ImageFileName = "test.png",
                Sold = false


            };

            repo.Insert(test);

            test.VehicleMakeId = 3;
            test.VehicleModelId = 3;
            test.VehicleTypeId = 1;
            test.BodyStyleId = 1;
            test.TransmissionTypeId = 1;
            test.ColorId = 2;
            test.InteriorColorId = 4;
            test.MSRP = 90000M;
            test.SalePrice = 86500M;
            test.Description = "Edited Car";
            test.FeaturedVehicle = false;
            test.ImageFileName = "update.png";
            test.Sold = true;

            repo.Update(test);


            var vehicle = repo.GetById(9);


            Assert.AreEqual(3, vehicle.VehicleMakeId);
            Assert.AreEqual(3, vehicle.VehicleModelId);
            Assert.AreEqual(1, vehicle.VehicleTypeId);
            Assert.AreEqual(1, vehicle.BodyStyleId);
            Assert.AreEqual(1, vehicle.TransmissionTypeId);
            Assert.AreEqual(2020, vehicle.Year);
            Assert.AreEqual(2, vehicle.ColorId);
            Assert.AreEqual(4, vehicle.InteriorColorId);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual("test", vehicle.VIN);
            Assert.AreEqual(90000M, vehicle.MSRP);
            Assert.AreEqual(86500M, vehicle.SalePrice);
            Assert.AreEqual("Edited Car", vehicle.Description);
            Assert.AreEqual(false, vehicle.FeaturedVehicle);
            Assert.AreEqual("update.png", vehicle.ImageFileName);
            Assert.AreEqual(true, vehicle.Sold);

        }

        [Test]
        public void CanDeleteVehicleListing()
        {
            var repo = new VehicleListingRepository();

            VehicleListing test = new VehicleListing()
            {
                VehicleMakeId = 1,
                VehicleModelId = 1,
                VehicleTypeId = 2,
                BodyStyleId = 3,
                TransmissionTypeId = 2,
                Year = 2020,
                ColorId = 3,
                InteriorColorId = 1,
                Mileage = 0,
                VIN = "test",
                MSRP = 99000M,
                SalePrice = 92000M,
                Description = "New Car",
                FeaturedVehicle = true,
                ImageFileName = "test.png",
                Sold = false


            };

            repo.Insert(test);


            var vehicle = repo.GetById(9);

            Assert.IsNotNull(vehicle);

            repo.Delete(9);

            vehicle = repo.GetById(9);

            Assert.IsNull(vehicle);

  

        }

        [Test]
        public void CanLoadModelByMakeId()
        {
            var repo = new VehicleModelRepository();
            var model = repo.GetByMakeId(2).ToList();

            Assert.IsNotNull(model);
            Assert.AreEqual("Soul", model[0].VehicleModelName);

        }
    }
}
