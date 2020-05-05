using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class StoreApp
    {
        #region Private Fields
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Public Methods
        public void StartApp()
        {
            IInterface ui;
            do
            {
                int selection;
                do
                {
                    PrintMainMenu();
                } while (!(int.TryParse(Console.ReadLine(), out selection)) && (selection != 1 || selection !=2));

                ui = ProcessSelection(selection);
            } while (ui.Run());

            //ManageUserSection();

            //ManageStoreSection();

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

        public void PrintMainMenu()
        {
            Console.Clear();
            //PrintTitle();
            Console.WriteLine("Choose user type:\n");
            Console.WriteLine("1. Admin.");
            Console.WriteLine("2. Customer.");
        }

        public IInterface ProcessSelection(int selection)
        {
            IInterface ui = null;
            switch (selection)
            {
                case 1:
                    ui = new AdminUI();
                    break;
                case 2:
                    ui = new CustomerUI();
                    break;
                default:
                    break;
            }
            return ui;
        }

        #region Private Methods
        private void DisplayWelcomeScreen()
        {
            string s = "***Welcome to THE store***";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
        }

        private void ManageMainMenu()
        {

        }

        #region User Login/Creation Methods
        private bool ManageUserSection()
        {
            var success = true;
            /*

            //Ask the user to 1. Log in 2. create an account
            var selection = GetUserLoginSelction();
            //Process the user selection
           // var customer = ProcessUserLogin(selection);
            if (customer == null)
            {
                Console.WriteLine("Unable to find a matching customer account.");
                //LoginUser();
            }

            //prompt user if there is an issue with account/creation
            //Go back to log in or creation???
            DisplayWelcomeUser(customer.FirstName);

            */
            return success;
        }
        /*
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
        */
        private int GetUserLoginSelction()
        {
            var selection = 0;
            do
            {
                Console.Write("Please enter 1 to log into an existing account or 2 to create a new account. ");

                int.TryParse(utils.ProcessInput(), out selection);
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

                int.TryParse(utils.ProcessInput(), out selection);
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

        
        #endregion
    }
}
