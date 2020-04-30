using P0DatabaseApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class StoreApp
    {
        #region Private Fields
        P0DbContext db = new P0DbContext();
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
        #endregion

        #region Private Methods
        private void DisplayWelcomeScreen()
        {
            Console.WriteLine("***Welcome to THE store***");
        }

        #region User Login/Creation Methods
        private bool ManageUserSection()
        {
            var success = true;

            //Ask the user to 1. Log in 2. create an account
            var selection = GetUserLoginSelction();
            //Process the user selection
            var displayName = ProcessUserLogin(selection);
            //prompt user if there is an issue with account/creation
            //Go back to log in or creation???
            DisplayWelcomeUser(displayName);

            return success;
        }

        private string ProcessUserLogin(int selection)
        {
            var displayName = string.Empty;
            if (selection == 1)//1. verify the account
            {
                displayName = LoginUser();
            }
            else//2. Create the user
            {
                displayName = RegisterNewUser();
            }
            return displayName;
        }
        private string LoginUser()
        {
            var fullName = string.Empty;
            Console.Write("Enter user name: ");
            var userName = ProcessInput();

            //retrieve the user information from the backend api(?)

            return fullName;
        }
        private string RegisterNewUser()
        {
            var fullName = string.Empty;

            Console.Write("Enter first name: ");
            var firstName = ProcessInput();
            Console.Write("Enter last name: ");
            var lastName = ProcessInput();
            Console.Write("Enter phone number: ");
            var phoneNumber = ProcessInput();

            //Send this information to the backend api

            return fullName;
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

        private int GetStoreSelection()
        {
            var selection = 0;

            //Get list of stores
            var stores = this.GetStoreList();

            for (int i = 0; i < stores.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, stores[i]);
            }
            do
            {
                Console.Write("Select store number to start shopping. ");

                int.TryParse(ProcessInput(), out selection);
            } while (!(selection > 0 && selection < stores.Length));

            return selection;
        }

        private string[] GetStoreList()
        {
            return new string[0];
        }

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
