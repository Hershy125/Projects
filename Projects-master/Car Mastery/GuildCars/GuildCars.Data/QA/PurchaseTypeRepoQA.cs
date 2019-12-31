using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class PurchaseTypeRepoQA : IPurchaseTypeRepository
    {
        public List<PurchaseType> GetAll()
        {
            return _purchaseTypes;
        }

        public PurchaseType GetById(int typeId)
        {
            var purchaseType = (from type in _purchaseTypes
                             where type.PurchaseTypeId == typeId
                             select type).FirstOrDefault();

            return purchaseType;
        }

        private static PurchaseType _bankFinance = new PurchaseType()
        {
            PurchaseTypeId = 1,
            PurchaseTypeName = "Bank Finance"
        };

        private static PurchaseType _dealerFinance = new PurchaseType()
        {
            PurchaseTypeId = 2,
            PurchaseTypeName = "Dealer Finance"
        };

        private static PurchaseType _cash = new PurchaseType()
        {
            PurchaseTypeId = 3,
            PurchaseTypeName = "Cash"
        };

        private static List<PurchaseType> _purchaseTypes = new List<PurchaseType>()
        {
            _bankFinance,
            _dealerFinance,
            _cash
        };
    }
}
