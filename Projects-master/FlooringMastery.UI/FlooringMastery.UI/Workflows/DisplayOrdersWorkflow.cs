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
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            ConsoleIO.DisplayOrdersHeader();
            string orderDate = ConsoleIO.GetOrderDateFromUser("Enter the order date: ", false);

            LoadOrderListResponse response = manager.LoadOrderList(orderDate);

            if(response.Success)
            {
                ConsoleIO.DisplayOrdersHeader();
                ConsoleIO.PrintOrders(response.Orders, orderDate);
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
