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
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            ConsoleIO.NewOrderHeader();
            string orderDate = ConsoleIO.GetOrderDateFromUser("Please enter date for work to be done: ", true);

            ConsoleIO.NewOrderHeader();
            string customerName = ConsoleIO.GetStringFromUser("Please enter customer name: ");

            ConsoleIO.NewOrderHeader();
            ConsoleIO.PrintStateList();
            string state = ConsoleIO.GetStateAbbreviationFromUser("Please enter the two letter abbreviation for the state where the work is being done: ");

            ConsoleIO.NewOrderHeader();
            var products = manager.GetProductsList().ProductList;
            int productIndex = ConsoleIO.GetProductChoiceFromUser();

            ConsoleIO.NewOrderHeader();
            decimal area = ConsoleIO.GetAreaFromUser("Please enter the area of floor in square feet: ");

            ConsoleIO.NewOrderHeader();
            Order newOrder = new Order()
            {
                CustomerName = customerName,
                State = state,
                FloorType = products[productIndex].FloorType,
                Area = area
            };

            LoadOrderResponse response = manager.LoadNewOrder(newOrder, orderDate);

            if(response.Success)
            {
                ConsoleIO.PrintOrders(response.Order, orderDate);

                Console.WriteLine();

                Console.WriteLine("Would you like to place this order? (Y/N)");
                string confirm = Console.ReadLine().ToUpper();
                if(confirm == "Y")
                {
                    manager.AddNewOrder(response.Order, orderDate);
                    Console.WriteLine("Thank you, your order has been placed.");
                }

            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
