using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class ProductsRepository : IProductRepository
    {
        public ListProductResponse LoadProducts(string filePath)
        {
            ListProductResponse response = new ListProductResponse();
            response.Success = true;
            response.ProductList = new List<Product>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Product product = new Product();

                        string[] columns = line.Split(',');

                        product.FloorType = columns[0];
                        product.CostPerSquareFoot = decimal.Parse(columns[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                        response.ProductList.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
