using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        public OrderManager(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        public LoadOrderListResponse LoadOrderList(string orderDate)
        {
            LoadOrderListResponse response = new LoadOrderListResponse();

            if (!_orderRepository.OrderFileExists(orderDate))
            {
                response.Success = false;
                response.Message = $"No orders were placed on {orderDate}";
            }
            else
            {
                response = _orderRepository.LoadOrders(orderDate);
            }
            return response;
        }

        public LoadOrderResponse LoadNewOrder(Order order, string date)
        {
            LoadOrderResponse response = new LoadOrderResponse();
            GetTaxRateResponse taxInfo = GetTaxInfo(order.State);

            response.Order = order;

            if(response.Order.Area < 100)
            {
                response.Success = false;
                response.Message = "The minimum order area is 100 square feet!";
                return response;
            }
            if(!taxInfo.Success)
            {
                response.Success = false;
                response.Message = taxInfo.Message;
                return response;
            }
            if(DateTime.TryParse(date, out DateTime orderDate))
            {
                if(orderDate.CompareTo(DateTime.Now) <= 0)
                {
                    response.Success = false;
                    response.Message = "The Date for the work to be done must be in the future.";
                    return response;
                }
            }
            if(response.Order.CustomerName == "")
            {
                response.Success = false;
                response.Message = "Customer name cannot be blank.";
                return response;
            }
            else
            {
                response.Success = true;
                response.Order.OrderNumber = GetOrderNumber(date);
                CalculateOrderCosts(response.Order);
            }
            return response;
        }

        public void AddNewOrder(Order order, string orderDate)
        {
            var orders = new List<Order>();

            if (_orderRepository.OrderFileExists(orderDate))
            {
                orders = LoadOrderList(orderDate).Orders;
                orders.Add(order);
            }
            else
            {
                orders.Add(order);
            }

            _orderRepository.SaveOrders(orders, orderDate);
        }

        public GetTaxRateResponse GetTaxInfo(string stateAbbreviation)
        {
            ListTaxResponse taxResponse = _taxRepository.LoadTaxInfo(ConfigurationManager.AppSettings["TaxFilePath"].ToString());
            GetTaxRateResponse response = new GetTaxRateResponse();

            if (taxResponse.Success)
            {
                var taxInfo = taxResponse.TaxList;

                var stateCheck = (from tax in taxInfo
                                  where tax.StateAbbreviation == stateAbbreviation
                                  select tax).ToArray();

                if (stateCheck.Any())
                {
                    response.TaxRate = stateCheck[0].TaxRate;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "We cannot do work in that state!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = taxResponse.Message;
            }

            return response;
        }

        public GetProductResponse GetProductInfo(string type)
        {
            var listResponse = _productRepository.LoadProducts(ConfigurationManager.AppSettings["ProductFilePath"].ToString());
            GetProductResponse response = new GetProductResponse();

            if(listResponse.Success)
            {
                foreach (Product product in listResponse.ProductList)
                {
                    if (product.FloorType == type)
                    {
                        response.ProductInfo = product;
                        response.Success = true;
                    }
                }
            }
            else
            {
                response.Success = false;
                response.Message = listResponse.Message;
            }


            return response;
        }

        public ListProductResponse GetProductsList()
        {
            ListProductResponse response = _productRepository.LoadProducts(ConfigurationManager.AppSettings["ProductFilePath"].ToString());
            return response;
        }

        public int GetOrderNumber(string orderDate)
        {
            int orderCount = 1;

            if(!_orderRepository.OrderFileExists(orderDate))
            {
                return orderCount;
            }
            else
            {
                List<Order> orders = LoadOrderList(orderDate).Orders;

                foreach (Order order in orders)
                {
                    orderCount++;
                }
            }

            return orderCount;
        }

        public LoadOrderResponse LoadOrder(string orderDate, int orderNumber)
        {
            LoadOrderListResponse displayResponse = LoadOrderList(orderDate);
            LoadOrderResponse loadResponse = new LoadOrderResponse();

            if(displayResponse.Success)
            {
                foreach(Order order in displayResponse.Orders)
                {
                    if(order.OrderNumber == orderNumber)
                    {
                        loadResponse.Order = order;
                        loadResponse.Success = true;
                    }
                }
                if(loadResponse.Order == null)
                {
                    loadResponse.Success = false;
                    loadResponse.Message = "An order with that order number was not found.";
                }
            }
            else
            {
                loadResponse.Success = false;
                loadResponse.Message = displayResponse.Message;
            }

            return loadResponse;
        }

        public void EditOrder(Order order, int index, string orderDate)
        {
            List<Order> orders = LoadOrderList(orderDate).Orders;
            orders[index] = order;

            _orderRepository.SaveOrders(orders, orderDate);

        }

        public Order CalculateOrderCosts(Order order)
        {
            GetTaxRateResponse taxInfo = GetTaxInfo(order.State);
            GetProductResponse productResponse = GetProductInfo(order.FloorType);
            Product orderProduct = productResponse.ProductInfo;

            order.TaxRate = taxInfo.TaxRate;
            order.CostPerSquareFoot = orderProduct.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = orderProduct.LaborCostPerSquareFoot;
            order.MaterialCost = (orderProduct.CostPerSquareFoot * order.Area);
            order.LaborCost = (orderProduct.LaborCostPerSquareFoot * order.Area);
            order.Tax = (order.LaborCost + order.MaterialCost) * (taxInfo.TaxRate / 100);
            order.Total = order.LaborCost + order.MaterialCost + order.Tax;

            return order;


        }

        public void RemoveOrder(int index, string orderDate)
        {
            var orders = LoadOrderList(orderDate).Orders;

            orders.RemoveAt(index);

            _orderRepository.SaveOrders(orders, orderDate);
        }

        public Dictionary<string, string> GetStateList()
        {
            ListTaxResponse taxResponse = _taxRepository.LoadTaxInfo(ConfigurationManager.AppSettings["TaxFilePath"].ToString());
            Dictionary<string, string> stateList = new Dictionary<string, string>();

            foreach (TaxInfo tax in taxResponse.TaxList)
            {
                stateList.Add(tax.StateAbbreviation, tax.StateName);
            }

            return stateList;
        }
    }
}
