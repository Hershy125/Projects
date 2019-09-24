using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringMastery.Data;
using FlooringMastery.Data.TestRepositories;

namespace FlooringMastery.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                case "Test":
                    return new OrderManager(new TestOrderRepository(), new TestTaxRepository(), new TestProductRepository());
                case "Prod":
                    return new OrderManager(new ProdOrderRepository(), new TaxInfoRepository(), new ProductsRepository());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }

        }
    }
}
