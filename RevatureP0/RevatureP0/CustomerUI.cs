using P0DatabaseApi;
using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevatureP0
{
    public class CustomerUI : IInterface
    {
        StoreBackend db;
        CustomerInfo customer;

        public CustomerUI() { this.db = new StoreBackend(); }
        public bool Run()
        {
            bool contnue = false;
            do
            {
                PrintMainMenu();
                int selection;
                while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection == 1 || selection == 2))
                {
                    Console.WriteLine("Please enter 1 or 2.");
                };
                switch (selection)
                {
                    case 1:
                        contnue = LoginUser();
                        break;
                    case 2:
                        contnue = RegisterNewUser();
                        break;
                    default:
                        break;
                }
            } while (!contnue);

            ManageSubMenu();
            return true;
        }
        private void PrintMainMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. Login.");
            Console.WriteLine("2. Create new Account.");
        }

        private void PrintSubMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. View Order History.");
            Console.WriteLine("2. Place a New Order.");
            Console.WriteLine("3. Return to main menu.");
        }

        private void PrintTitle()
        {
            Console.WriteLine("Choose something.\n");
        }

        private bool LoginUser()
        {
            var okToContinue = true;

            var email = string.Empty;
            Console.Write("Enter email address: ");
            email = utils.ProcessInput();

            while (!IsValidEmail(email))
            {
                Console.WriteLine("Please enter a valid email address.");
                email = utils.ProcessInput();
            };

            //retrieve the user information from the backend api
            customer = db.GetCustomerInfo(email);
            if(customer == null)
            {
                okToContinue = false;
                Console.Write("Unable to find an account linked to that email. Press any key to return to the previous menu. ");
                Console.ReadLine();
            }
            return okToContinue;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool RegisterNewUser()
        {
            var okToContinue = true;
            Console.Write("Enter first name: ");
            var firstName = utils.ProcessInput();
            Console.Write("Enter last name: ");
            var lastName = utils.ProcessInput();
            Console.Write("Enter email address: ");
            var email = utils.ProcessInput();

            //Send this information to the backend api
            customer = db.AddNewCustomer(firstName, lastName, email);

            return okToContinue;
        }

        private void ManageSubMenu()
        {
            int selection;
            do
            {
                PrintSubMenu();
                while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection < 1 && selection > 3))
                {
                    Console.WriteLine("Please enter 1 to view order history, 2 to place a new order, or 3 to return to the main menu.");
                };

                switch (selection)
                {
                    case 1:
                        DisplayOrderHistory();
                        break;
                    case 2:
                        ManagePlacingOrder();
                        break;
                    default:
                        break;
                }
            } while (selection != 3);
        }
        private void DisplayOrderHistory()
        {
            Console.Clear();

            var orders = db.GetCustomerOrderHistory(customer.CustomerId);
            if (orders == null || orders.Count == 0)
                Console.WriteLine("You havent placed any orders yet.");
            else
            {
                foreach (var order in orders)
                {
                    var output = $"Order {order.OrderId} placed on {order.OrderDate.ToShortDateString()} at the {order.StoreLocation} location totalling {order.OrderTotal}";
                    Console.WriteLine(output);
                }
            }
            Console.Write("Press any key to return to the previous menu.");
            Console.ReadLine();
        }

        private void ManagePlacingOrder()
        {
            //Get locations
            Console.Clear();
            PrintTitle();

            var locations = db.GetAllLocations();
            for (int i = 0; i < locations.Count; i++)
            {
                Console.WriteLine($"{i+1}. {locations[i].Location}");
            }
            //have the user select a location
            int selection;
            do
            {
                Console.Write("Select a store number to view its available products. ");
            } while (int.TryParse(Console.ReadLine(), out selection) && (selection <0 || selection > locations.Count));
            //Display the products for that location
            Console.Clear();
            Console.WriteLine($"Inventory for location {locations[selection - 1].Location}\n");
            
            /*
            if (null == locations[selection - 1].AvailableProducts || locations[selection - 1].AvailableProducts.Count == 0)
            {
                Console.Write("There are no available products at that location. Press any key to return to the locations menu.");
                Console.ReadLine();

            }
            else
            {
            */
                foreach (var item in locations[selection - 1].AvailableProducts)
                {
                    Console.WriteLine($"{item.ProductId}. {item.ProductDescription} available quantity {item.Quantity}");
                }
            //}

            //Have the user add products and quantites to the order
            Console.WriteLine("To add an item to the order enter an item number followed by the quantity.");
            var items = new List<ProductQuantity>();
            string[] input;
            bool cont = true;
            do
            {
                var itemAlreadyAdded = false;
                input = Console.ReadLine().Split(' ');
                if(input.Length == 1)
                {
                    if(!(cont != string.IsNullOrEmpty(input[0])))
                        break;
                }
                if (input.Length == 2)
                {
                    foreach (var item in items)
                    {
                        if (item.ProductId == int.Parse(input[0]))
                        {
                            itemAlreadyAdded = true;
                            Console.WriteLine("That item has already been added to the order.");
                            break;
                        }
                    }
                    if (!itemAlreadyAdded)
                    {
                        items.Add(new ProductQuantity()
                        {
                            ProductId = int.Parse(input[0]),
                            Quantity = int.Parse(input[1])
                        });
                    }
                    Console.WriteLine("You can continue to add items or press enter to finish the order.");
                }
                else
                    Console.WriteLine("Enter the product number followed by a space followed by the desired quantity.");
            } while (cont);
            if(DisplayPreOrderTotals(locations[selection - 1], items))
            {
                var orderInfo = db.PlaceNewOrder(locations[selection - 1].StoreId, customer.CustomerId, items);
                DisplayNewOrder(orderInfo);
            }
            else
                Console.WriteLine("Order Canceled.");

            Console.Write("Press any key to return to previous menu.");
            Console.ReadLine();
        }
        private void DisplayNewOrder(OrderInfo orderInfo)
        {
            var output = $"Order {orderInfo.OrderId} placed on {orderInfo.OrderDate.ToShortDateString()} at the {orderInfo.StoreLocation} location totalling {orderInfo.OrderTotal}";
            Console.WriteLine(output);
            //Console.WriteLine("\n");

            foreach (var item in orderInfo.LineItems)
            {
                var lineTotal = item.PricePaid * item.Quantity;
                output = $"{item.Quantity} {item.ProductDescrition} for a total of {lineTotal}";
                Console.WriteLine("\t" + output);
            }
        }
        private bool DisplayPreOrderTotals(LocationInfo loc, List<ProductQuantity> items)
        {
            var total = 0.0;
            foreach (var item in items)
            {
                var prodInfo = (from inv in loc.AvailableProducts
                                where inv.ProductId == item.ProductId
                                select inv).First();

                var lineTotal = prodInfo.Price * item.Quantity;
                total += lineTotal;
                Console.WriteLine($"{item.Quantity} of product {item.ProductId} for a total of {lineTotal}");
            }
            Console.WriteLine($"\nOrder contains {items.Count} items for a total cost of {total}.");
            Console.Write("\nPlace this order (y/n)?");

            return "y" == Console.ReadLine().ToLower();
        }
    }
}