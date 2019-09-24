using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            bool recalculate = false;

            ConsoleIO.EditOrderHeader();
            string orderDate = ConsoleIO.GetOrderDateFromUser("Please enter the order date: ", false);

            LoadOrderListResponse orderListResponse = manager.LoadOrderList(orderDate);

            if(orderListResponse.Success)
            {
                int orderNumber = ConsoleIO.GetOrderNumberFromUser("Please enter the order number: ");

                LoadOrderResponse orderResponse = manager.LoadOrder(orderDate, orderNumber);

                if (orderResponse.Success)
                {
                    Order editOrder = orderResponse.Order;

                    Order originalOrder = new Order()
                    {
                        OrderNumber = editOrder.OrderNumber,
                        CustomerName = editOrder.CustomerName,
                        State = editOrder.State,
                        FloorType = editOrder.FloorType,
                        Area = editOrder.Area,
                        MaterialCost = editOrder.MaterialCost,
                        LaborCost = editOrder.LaborCost,
                        Tax = editOrder.Tax,
                        Total = editOrder.Total
                    };

                    ConsoleIO.EditOrderHeader();
                    ConsoleIO.PrintOrders(editOrder, orderDate);

                    Console.WriteLine("Would you like to edit the customer name? Press 'Y' to edit. Press any other key to skip");
                    string userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y")
                    {
                        editOrder.CustomerName = ConsoleIO.GetStringFromUser("Please enter the new customer name: ");
                    }

                    ConsoleIO.EditOrderHeader();
                    ConsoleIO.PrintOrders(editOrder, orderDate);

                    Console.WriteLine("Would you like to change the state where the work is being done? Press 'Y' to edit. Press any other key to skip.");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y")
                    {
                        ConsoleIO.PrintStateList();
                        editOrder.State = ConsoleIO.GetStateAbbreviationFromUser("Please enter the two letter abbreviation for the state where the work is being done: ");
                        recalculate = true;
                    }

                    ConsoleIO.EditOrderHeader();
                    ConsoleIO.PrintOrders(editOrder, orderDate);

                    Console.WriteLine("Would you like to change the flooring material? Press 'Y' to edit. Press any other key to skip.");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y")
                    {
                        var products = manager.GetProductsList().ProductList;
                        int productIndex = ConsoleIO.GetProductChoiceFromUser();
                        editOrder.FloorType = products[productIndex].FloorType;
                        recalculate = true;
                    }

                    ConsoleIO.EditOrderHeader();
                    ConsoleIO.PrintOrders(editOrder, orderDate);

                    Console.WriteLine("Would you like to change the flooring area? Press 'Y' to edit. Press any other key to skip.");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y")
                    {
                        editOrder.Area = ConsoleIO.GetAreaFromUser("Please enter the new square footage: ");
                        recalculate = true;
                    }

                    if (recalculate)
                    {
                        manager.CalculateOrderCosts(editOrder);
                    }

                    ConsoleIO.EditOrderHeader();
                    Console.WriteLine("Original Order: ");
                    ConsoleIO.PrintOrders(originalOrder, orderDate);

                    Console.WriteLine();

                    Console.WriteLine("Edits Made: ");
                    ConsoleIO.PrintOrders(editOrder, orderDate);

                    Console.WriteLine();

                    Console.WriteLine("Confirm changes to order? (Y/N)");
                    string confirm = Console.ReadLine().ToUpper();

                    if (confirm == "Y")
                    {
                        Console.WriteLine("Your changes have been made.");
                        manager.EditOrder(editOrder, editOrder.OrderNumber - 1, orderDate);
                    }

                }
                else
                {
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(orderResponse.Message);
                }
            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(orderListResponse.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
