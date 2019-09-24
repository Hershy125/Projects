using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class ProdOrderRepository : IOrderRepository
    {
        public void SaveOrders(List<Order> orders, string orderDate)
        {
            string _filePath = GetFilePath(orderDate);

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath); 
            }
            if (orders.Count() > 0)
            {
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot," +
                        "LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        writer.WriteLine(CreateCsvForOrder(order));
                    }
                }
            }

        }


        public LoadOrderListResponse LoadOrders(string orderDate)
        {
            LoadOrderListResponse response = new LoadOrderListResponse();
            response.Success = true;
            response.Orders = new List<Order>();

            string _filePath = GetFilePath(orderDate);

            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Order newOrder = new Order();

                        string[] columns = line.Split(',');

                        newOrder.OrderNumber = int.Parse(columns[0]);
                        newOrder.CustomerName = columns[1];
                        newOrder.State = columns[2];
                        newOrder.TaxRate = decimal.Parse(columns[3]);
                        newOrder.FloorType = columns[4];
                        newOrder.Area = decimal.Parse(columns[5]);
                        newOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                        newOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        newOrder.MaterialCost = decimal.Parse(columns[8]);
                        newOrder.LaborCost = decimal.Parse(columns[9]);
                        newOrder.Tax = decimal.Parse(columns[10]);
                        newOrder.Total = decimal.Parse(columns[11]);

                        response.Orders.Add(newOrder);

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

        public bool OrderFileExists(string orderDate)
        {
            if (File.Exists(GetFilePath(orderDate)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private string CreateCsvForOrder(Order order)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                order.OrderNumber.ToString(), order.CustomerName, order.State, order.TaxRate.ToString(), order.FloorType,
                order.Area.ToString(), order.CostPerSquareFoot.ToString(), order.LaborCostPerSquareFoot.ToString(), order.MaterialCost.ToString(),
                order.LaborCost.ToString(), order.Tax.ToString(), order.Total.ToString());
        }


        private string GetFilePath(string orderDate)
        {
            var original = DateTime.Parse(orderDate);
            string fileDate = original.ToString("MMddyyyy");
            string filepath = ConfigurationManager.AppSettings["OrdersFilePath"].ToString();
            return filepath + fileDate + ".txt";
        }
    }
}
