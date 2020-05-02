using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class StoreApp
    {
        #region Private Fields
        StoreBackend db = new StoreBackend();
        const string _EXIT = "EXIT";

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Public Methods
        public void StartApp()
        {
            //Welcome the user
            DisplayWelcomeScreen();

            ManageUserSection();

            ManageStoreSection();

            //Create a menu of options for that store
            //1. View available products
            


            //2. Add product to "cart"
            //Verify availablity/correctness
            //prompt user of either sucess or reason for failure
            //3. Checkout
            //Display final order and totals
            //Return to store menu
            //4. Return to store selction menu

            //Allow for "Exit" to close program at any time???
        }
        #endregion

        #region Private Methods
        private void DisplayWelcomeScreen()
        {
            Console.WriteLine("***Welcome to THE store***");
        }

        private void ManageMainMenu()
        {

        }

        #region User Login/Creation Methods
        private bool ManageUserSection()
        {
            var success = true;

            //Ask the user to 1. Log in 2. create an account
            var selection = GetUserLoginSelction();
            //Process the user selection
            var customer = ProcessUserLogin(selection);
            if (customer == null)
            {
                Console.WriteLine("Unable to find a matching customer account.");
                LoginUser();
            }

            //prompt user if there is an issue with account/creation
            //Go back to log in or creation???
            DisplayWelcomeUser(customer.FirstName);

            return success;
        }

        private CustomerInfo ProcessUserLogin(int selection)
        {
            CustomerInfo cust = null;
            if (selection == 1)//1. verify the account
            {
                cust = LoginUser();
            }
            else//2. Create the user
            {
                cust = RegisterNewUser();
            }
            return cust;
        }
        private CustomerInfo LoginUser()
        {
            Console.Write("Enter first name: ");
            var fName = ProcessInput();
            Console.Write("Enter last name: ");
            var lName = ProcessInput();

            //retrieve the user information from the backend api
            var cust = db.GetCustomer(fName, lName);
            return cust;
        }
        private CustomerInfo RegisterNewUser()
        {
            Console.Write("Enter first name: ");
            var firstName = ProcessInput();
            Console.Write("Enter last name: ");
            var lastName = ProcessInput();
            Console.Write("Enter phone number: ");
            var phoneNumber = ProcessInput();

            //Send this information to the backend api
            var cust = db.CreateNewCustomer(firstName, lastName, phoneNumber);
            return cust;
        }
        private int GetUserLoginSelction()
        {
            var selection = 0;
            do
            {
                Console.Write("Please enter 1 to log into an existing account or 2 to create a new account. ");

                int.TryParse(ProcessInput(), out selection);
            } while (!(selection == 1 || selection == 2));

            return selection;
        }

        private void DisplayWelcomeUser(string name)
        {
            Console.WriteLine($"Welcome {name}");
        }
        #endregion

        #region Store Selection/Info Methods

        private bool ManageStoreSection()
        {
            var success = true;

            //Get list of stores
            var stores = this.GetStoreList();
            var selection = GetStoreSelection(stores);
            var inventory = GetStoreInventory(stores[selection - 1]);
            
            DisplayStoreInventory(inventory);

            return success;
        }
        private int GetStoreSelection(string[] stores)
        {
            var selection = 0;

            //Ask the user to select a store location (menu of n choices)
            for (int i = 0; i < stores.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, stores[i]);
            }
            do
            {
                Console.Write("Select store number to get started. ");

                int.TryParse(ProcessInput(), out selection);
            } while (!(selection > 0 && selection <= stores.Length));

            return selection;
        }
        private string[] GetStoreList()
        {
            var stores = new string[]
            {
                "Seattle",
                "Tacoma",
                "Bellevue",
            };
            return stores;
        }

        private string[,] GetStoreInventory(string storeLocation)
        {
            var inventory = new string[,] 
            { 
                { "Product1", "10" }, 
                { "Product2", "15" }, 
                { "Product3", "2" } 
            };

            return inventory;
        }
        private void DisplayStoreInventory(string[,] inventory)
        {
            for (int i = 0; i < inventory.GetLength(0); i++)
            {
                Console.WriteLine($"{i + 1}. {inventory[i, 0]} available quantity - {inventory[i, 1]}");
            }
        }

        #endregion

        private string ProcessInput()
        {
            var input = Console.ReadLine();
            CheckForExit(input);
            return input;
        }
        private void CheckForExit(string input)
        {
            if (input.ToUpper() == _EXIT)
            {
                System.Environment.Exit(0);
            }
        }
        #endregion
    }
}
