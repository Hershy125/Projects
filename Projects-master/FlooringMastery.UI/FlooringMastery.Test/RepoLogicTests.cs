using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Test
{
    [TestFixture]
    public class DisplayOrderTest
    {
        private const string _filePath = @"C:\data\FlooringMastery\Orders_10102020.txt";
        private const string _original = @"C:\data\FlooringMastery\Test_10102020.txt";

        [SetUp]
        public void Setup()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            if (mode == "Prod")
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                File.Copy(_original, _filePath);
            }
            else
            {
                OrderManager manager = OrderManagerFactory.Create();
                LoadOrderResponse orderResponse = manager.LoadOrder("10/10/20", 1);
                Order testOrder = orderResponse.Order;

                testOrder.OrderNumber = 1;
                testOrder.CustomerName = "Dave";
                testOrder.State = "MI";
                testOrder.TaxRate = 5.75M;
                testOrder.FloorType = "Wood";
                testOrder.Area = 100M;
                testOrder.CostPerSquareFoot = 5.15M;
                testOrder.LaborCostPerSquareFoot = 4.75M;
                testOrder.MaterialCost = 515.00M;
                testOrder.LaborCost = 475.00M;
                testOrder.Tax = 56.93M;
                testOrder.Total = 1046.925M;

            }

            
        }

        [Test]
        public void CanLoadTestOrders()
        {
            OrderManager manager = OrderManagerFactory.Create();

            LoadOrderListResponse response = manager.LoadOrderList("10/10/20");

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Orders.Count());

            var orders = response.Orders;

            Order check = orders[0];

            Assert.AreEqual("Dave", check.CustomerName);
            Assert.AreEqual("Wood", check.FloorType);
            Assert.AreEqual(1046.925, check.Total);
            Assert.AreEqual(100, check.Area);


        }
        
    


        [Test]
        [TestCase("", "OH", "Carpet", 120, "11/20/2019", false)]
        [TestCase("Johnny V", "PA", "Laminate", 90, "6/19/2019", false)]
        [TestCase("Bobby B", "GA", "Tile", 145, "7/04/2019", false)]
        [TestCase("Ho Chi Min", "IN", "Wood", 200, "8/12/2018", false)]
        [TestCase("Test Man", "MI", "Tile", 105, "10/10/2020", true)]
        public void CanAddOrderToRepo(string customerName, string state, string type, int area, string orderDate, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();

            Order newOrder = new Order()
            {
                CustomerName = customerName,
                State = state,
                FloorType = type,
                Area = area

            };

            LoadOrderResponse addResponse = manager.LoadNewOrder(newOrder, orderDate);

            Assert.AreEqual(expectedResult, addResponse.Success);

            if(addResponse.Success)
            {
                manager.AddNewOrder(newOrder, orderDate);

                LoadOrderListResponse response = manager.LoadOrderList("10/10/20");
                var orders = response.Orders;

                Order check = orders[1];

                Assert.AreEqual("Test Man", check.CustomerName);
                Assert.AreEqual("Tile", check.FloorType);
                Assert.AreEqual(105, check.Area);
            }

        }


        [Test]
        [TestCase(1, "John", "PA", "Carpet", 100)]
        public void CanEditExistingOrder(int orderNumber, string customerName, string state, string type, int area)
        {
            OrderManager manager = OrderManagerFactory.Create();

            LoadOrderResponse orderResponse = manager.LoadOrder("10/10/20", orderNumber);
            var editOrder = orderResponse.Order;

            Assert.AreEqual("Dave", editOrder.CustomerName);
            Assert.AreEqual("MI", editOrder.State);
            Assert.AreEqual("Wood", editOrder.FloorType);
            Assert.AreEqual(100, editOrder.Area);


            editOrder.CustomerName = customerName;
            editOrder.State = state;
            editOrder.FloorType = type;
            editOrder.Area = area;

            manager.CalculateOrderCosts(editOrder);
            manager.EditOrder(editOrder, editOrder.OrderNumber - 1, "10/10/20");

            var orders = manager.LoadOrderList("10/10/20").Orders;

            Assert.AreEqual(customerName, orders[0].CustomerName);
            Assert.AreEqual(state, orders[0].State);
            Assert.AreEqual(type, orders[0].FloorType);
            Assert.AreEqual(area, orders[0].Area);



        }
    


        [Test]
        [TestCase(0, "10/10/20")]
        public void CanRemoveOrder(int index, string orderDate)
        {
            OrderManager manager = OrderManagerFactory.Create();
            var orders = manager.LoadOrderList(orderDate).Orders;

            Assert.AreEqual(1, orders.Count());

            manager.RemoveOrder(index, orderDate);

            var response = manager.LoadOrder(orderDate, 1);

            Assert.IsFalse(response.Success);


        }
    }
}
