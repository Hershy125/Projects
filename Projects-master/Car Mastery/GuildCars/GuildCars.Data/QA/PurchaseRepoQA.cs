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
    public class PurchaseRepoQA : IPurchaseRepository
    {
        public List<Purchase> GetAll()
        {
            return _purchases;
        }

        public void Insert(Purchase purchase)
        {

            var lastId = _purchases.MaxBy(x => x.PurchaseId).FirstOrDefault();
            int newId = lastId.PurchaseId + 1;

            purchase.PurchaseId = newId;
            purchase.CreatedDate = DateTime.Now;

            _purchases.Add(purchase);
        }


        public List<Purchase> GetByUserId(string userId)
        {
            var purchases = (from purchase in _purchases
                            where purchase.UserId == userId
                            select purchase).ToList();

            return purchases;
        }

        private static Purchase _purchase1 = new Purchase()
        {
            PurchaseId = 1,
            VehicleListingId = 1,
            CustomerId = 1,
            UserId = "00000000-0000-0000-0000-000000000000",
            PurchasePrice = 22000.00M,
            PurchaseTypeId = 2,
            CreatedDate = new DateTime(2019, 12, 8)
            
        };

        private static Purchase _purchase2 = new Purchase()
        {
            PurchaseId = 2,
            VehicleListingId = 2,
            CustomerId = 1,
            UserId = "00000000-0000-0000-0000-000000000000",
            PurchasePrice = 32000.00M,
            PurchaseTypeId = 1,
            CreatedDate = new DateTime(2019, 12, 10)
        };

        private static Purchase _purchase3 = new Purchase()
        {
            PurchaseId = 3,
            VehicleListingId = 3,
            CustomerId = 1,
            UserId = "00000000-0000-0000-0000-000000000000",
            PurchasePrice = 12000.00M,
            PurchaseTypeId = 3,
            CreatedDate = new DateTime(2019, 12, 12)
        };

        private static List<Purchase> _purchases = new List<Purchase>()
        {
            _purchase1,
            _purchase2,
            _purchase3

        };
    }
}
