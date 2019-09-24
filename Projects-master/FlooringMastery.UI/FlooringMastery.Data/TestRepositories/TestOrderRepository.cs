using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class TestOrderRepository : IOrderRepository
    {

        public void SaveOrders(List<Order> orders, string orderDate)
        {
            _orders = orders;
        }

        public LoadOrderListResponse LoadOrders(string orderDate)
        {
            LoadOrderListResponse response = new LoadOrderListResponse();
            response.Orders = _orders;
            response.Success = true;
            return response;
        }

        public bool OrderFileExists(string orderDate)
        {
            return true;
        }

        private static Order testOrder = new Order()
        {
            OrderNumber = 1,
            CustomerName = "Dave",
            State = "MI",
            TaxRate = 5.75M,
            FloorType = "Wood",
            Area = 100M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            MaterialCost = 515.00M,
            LaborCost = 475.00M,
            Tax = 56.93M,
            Total = 1046.93M
        };

        private static List<Order> _orders = new List<Order>()
        {
            testOrder
        };


    }
}
