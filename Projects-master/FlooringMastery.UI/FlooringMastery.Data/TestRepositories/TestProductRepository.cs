using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.TestRepositories
{
    public class TestProductRepository : IProductRepository
    {
        public ListProductResponse LoadProducts(string filePath)
        {
            ListProductResponse response = new ListProductResponse();
            response.Success = true;
            response.ProductList = _products;

            return response;
        }

        private static readonly Product _wood = new Product()
        {
            FloorType = "Wood",
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M
        };
        private static readonly Product _carpet = new Product()
        {
            FloorType = "Carpet",
            CostPerSquareFoot = 2.25M,
            LaborCostPerSquareFoot = 2.10M
        };
        private static readonly Product _laminate = new Product()
        {
            FloorType = "Laminate",
            CostPerSquareFoot = 1.75M,
            LaborCostPerSquareFoot = 2.10M
        };
        private static readonly Product _tile = new Product()
        {
            FloorType = "Tile",
            CostPerSquareFoot = 3.50M,
            LaborCostPerSquareFoot = 4.15M
        };

        private static List<Product> _products = new List<Product>()
        {
            _wood,
            _carpet,
            _laminate,
            _tile
        };
    }
}
