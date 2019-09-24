using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("*********************************************");
            string orderDate = ConsoleIO.GetOrderDateFromUser("Enter the order date: ", true);

            LoadOrderListResponse orderListResponse = manager.LoadOrderList(orderDate);

            if (orderListResponse.Success)
            {
                int orderNumber = ConsoleIO.GetOrderNumberFromUser("Please enter the order number: ");
                int orderIndex = orderNumber - 1;

                LoadOrderResponse orderResponse = manager.LoadOrder(orderDate, orderNumber);

                if (orderResponse.Success)
                {
                    Console.Clear();
                    ConsoleIO.PrintOrders(orderResponse.Order, orderDate);
                    Console.WriteLine();

                    Console.WriteLine("Are you sure you want to cancel this order? (Y/N)");
                    string confirm = Console.ReadLine().ToUpper();

                    if (confirm == "Y")
                    {
                        Console.WriteLine("This order has been cancelled.");
                        manager.RemoveOrder(orderIndex, orderDate);
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
