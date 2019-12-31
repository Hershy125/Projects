using GuildCars.Data.Production;
using GuildCars.Models.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.Integration_Tests
{
    [TestFixture]
    public class SearchADOTests
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
        public void CanSearchOnMinMSRP()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { MinMSRP = 20000M });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchOnMaxMSRP()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { MaxMSRP = 30000M });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchOnVehicleType()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { VehicleTypeId = 2 });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnMSRPRange()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { MinMSRP = 20000M, MaxMSRP = 30000M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnPartialMakeTerm()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { SearchTerm = "Fo" });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnAllFilters()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters { SearchTerm = "Fo", MinMSRP = 30000M, MaxMSRP = 40000M, MinYear = 2013, MaxYear = 2017 });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchOnNoFilters()
        {
            var repo = new VehicleListingRepository();

            var found = repo.GetSearchResults(new ListingSearchParameters());

            Assert.AreEqual(6, found.Count());
        }


    }
}
