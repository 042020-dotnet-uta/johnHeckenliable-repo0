using System;
using System.Collections.Generic;
using P0DatabaseApi;

namespace RevatureP0
{
    class Program
    {
        const string _EXIT = "EXIT";
        private static StoreApp controller;
        static void Main(string[] args)
        {
            controller = new StoreApp();
            //Welcome the user
            Console.WriteLine("***Welcome to THE store***");

            P0DbContext db = new P0DbContext();
            /*
            Console.WriteLine("\nPress enter to fake some data...");
            Console.ReadLine();
            //db.CreateSomeData();
            Console.WriteLine("Test data created.");
            Console.WriteLine("\nPress enter to run some queries...");
            Console.ReadLine();
            //db.QuerySomeData();
            */

            Console.WriteLine("\nPress enter to change some data...");
            Console.ReadLine();
            //db.UpdatePhoneNumber("April", "Showers", "555-555-5555");
            //db.UpdateProductQuantity(1, 1, -5);

            var products = new Dictionary<int, int>();
            products.Add(1, 5);
            products.Add(4, 6);
            products.Add(5, 10);

            db.CreateOrder(1, 1, products);

            //Ask the user to 1. Log in 2. create an account

            //1. verify the account
            //2. Create the user
            //prompt user if there is an issue with account/creation
            //Go back to log in or creation???

            //Ask the user to select a store location (menu of n choices)

            //var store = ProcessStoreSelection(selection);

            //Create a menu of options for that store
            //1. View available products
            //selection = GetAvailableProducts(store);


            //2. Add product to "cart"
            //Verify availablity/correctness
            //prompt user of either sucess or reason for failure
            //3. Checkout
            //Display final order and totals
            //Return to store menu
            //4. Return to store selction menu

            //Allow for "Exit" to close program at any time???
        }

        //private static int GetAvailableProducts(Store store)
        //{
        //    var selection = 0;
        //    var i = 1;
        //    foreach (var product in store.AvailableProducts)
        //    {
        //        //Console.WriteLine($"{i}. {product.Key.ProductDescription}, Quantity {product.Value}");
        //        i++;
        //    }

        //    do
        //    {
        //        Console.Write("Select store number to start shopping. ");

        //        int.TryParse(ProcessInput(), out selection);
        //    } while (!(selection > 0 && selection < store.AvailableProducts.Count));

        //    return selection;
        //}

        private static int GetStoreSelection()
        {
            var selection = 0;

            //Get list of stores
            var stores = controller.GetStoreList();

            for (int i = 0; i < stores.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i+1, stores[i]);
            }
            do
            {
                Console.Write("Select store number to start shopping. ");

                int.TryParse(ProcessInput(), out selection);
            } while (!(selection >0 && selection < stores.Length));

            return selection;
        }

        private static void ProcessUserLogin(int selection)
        {
            if(selection == 1)
            {
                LoginUser();
            }
            else
            {
                Console.WriteLine("User Account Creation.");
            }
        }
        private static void LoginUser()
        {
            Console.Write("Enter user name: ");
            var userName = ProcessInput();
        }
        private static void RegisterUser()
        {
            Console.Write("Enter first name: ");
            var firstName = ProcessInput();
            Console.Write("Enter last name: ");
            var lastName = ProcessInput();
            Console.Write("Enter phone number: ");
            var phoneNumber = ProcessInput();
        }

        private static int GetUserLoginSelction()
        {
            var selection = 0;
            do
            {
                Console.Write("Please enter 1 to log into an existing account or 2 to create a new account. ");

                int.TryParse(ProcessInput(), out selection);
            } while (!(selection == 1 || selection == 2));

            return selection;
        }
        private static string ProcessInput()
        {
            var input = Console.ReadLine();
            CheckForExit(input);
            return input;
        }
        private static void CheckForExit(string input)
        {
            if(input.ToUpper() == _EXIT)
            {
                System.Environment.Exit(0);
            }
        }
    }
}