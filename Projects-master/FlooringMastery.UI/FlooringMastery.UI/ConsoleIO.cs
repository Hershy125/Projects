using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class ConsoleIO
    {
        public const string PickProductLineFormat = "{0, 2} {1, -10} {2, 25:c} {3, 25:c}";

        public static void PrintOrders(List<Order> orders, string orderDate)
        {
            foreach (Order order in orders)
            {
                Console.WriteLine("***********************************************************");
                Console.WriteLine(order.OrderNumber + " " + orderDate);
                Console.WriteLine(order.CustomerName);
                Console.WriteLine(order.State);
                Console.WriteLine($"Product: {order.FloorType}");
                Console.WriteLine($"Area: {order.Area} sq. ft");
                Console.WriteLine($"Materials: {order.MaterialCost:c}");
                Console.WriteLine($"Labor {order.LaborCost:c}");
                Console.WriteLine($"Tax: {order.Tax:c}");
                Console.WriteLine($"Total: {order.Total:c}");
                Console.WriteLine("***********************************************************");
            }
        }

        public static void PrintOrders(Order order, string orderDate)
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine(order.OrderNumber + " " + orderDate);
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Product: {order.FloorType}");
            Console.WriteLine($"Area: {order.Area} sq. ft");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine("***********************************************************");
        }

        public static void PrintPickListOfProducts(List<Product> products)
        {
            Console.WriteLine(PickProductLineFormat, "", "Floor Type", "Cost Per Sq. Ft.", "Labor Cost Per Sq. Ft.");
            Console.WriteLine("==========================================================================");

            for (int i = 0; i < products.Count(); i++)
            {
                Console.WriteLine(PickProductLineFormat, i + 1, products[i].FloorType, products[i].CostPerSquareFoot,
                    products[i].LaborCostPerSquareFoot);
            }
            Console.WriteLine();
        }

        public static int GetIndexFromUser(string prompt, int count)
        {
            int output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                }
                else
                {
                    if (output < 1 || output > count)
                    {
                        Console.WriteLine("That is not a valid selection");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }

        public static decimal GetAreaFromUser(string prompt)
        {
            decimal output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                }
                else
                {
                    if (output < 100)
                    {
                        Console.WriteLine("Order minimum is 100 square feet!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }

        public static string GetOrderDateFromUser(string prompt, bool newOrder)
        {
            DateTime output;
            while(true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if(!DateTime.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (newOrder && (output.CompareTo(DateTime.Now) <= 0))
                    {
                        Console.WriteLine("Date must be in the future.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    return input;
                }
            }
        }

        public static string GetStringFromUser(string prompt)
        {
            while(true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if(input == "")
                { 
                    Console.WriteLine("This field cannot be blank.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else 
                {
                    if (input.Contains(","))
                    {
                        Console.WriteLine("This field cannot contain commas.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    return input;

                }

                
            }
        }

        public static string GetStateAbbreviationFromUser(string prompt)
        {
            OrderManager manager = OrderManagerFactory.Create();

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                GetTaxRateResponse response = manager.GetTaxInfo(input);

                if(!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                return input;
            }
        }

        public static int GetProductChoiceFromUser()
        {
            OrderManager manager = OrderManagerFactory.Create();

            var products = manager.GetProductsList().ProductList;

            Console.WriteLine();

            PrintPickListOfProducts(products);

            Console.WriteLine();
            Console.WriteLine();

            int index = GetIndexFromUser("What floor type would you like? ", products.Count());
            index--;

            return index;

        }

        public static void NewOrderHeader()
        {
            Console.Clear();
            Console.WriteLine("Place New Order");
            Console.WriteLine("*********************************************");
        }

        public static void EditOrderHeader()
        {
            Console.Clear();
            Console.WriteLine("Edit Order");
            Console.WriteLine("*********************************************");
        }

        public static void DisplayOrdersHeader()
        {
            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("*********************************************");
        }

        public static int GetOrderNumberFromUser(string prompt)
        {
            int output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                }
             
                return output;

            }
        }

        public static void PrintStateList()
        {
            OrderManager manager = OrderManagerFactory.Create();

            var stateList = manager.GetStateList();

            Console.WriteLine(" ________________________________________________________");
            Console.WriteLine("| We are currently only accepting orders from customers");
            Console.WriteLine("| in the following states:");
            foreach(var pair in stateList)
            {
                Console.WriteLine($"| {pair.Value} - {pair.Key}");
            }
            Console.WriteLine("| We apologize for the inconvenience.");
            Console.WriteLine("|________________________________________________________");
        }
    }
}
