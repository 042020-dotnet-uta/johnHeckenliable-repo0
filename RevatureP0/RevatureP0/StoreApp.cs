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
            Console.WriteLine("***Welcome to THE store***");

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

        #endregion


        #region Private Methods
        private static string ProcessInput()
        {
            var input = Console.ReadLine();
            CheckForExit(input);
            return input;
        }
        private static void CheckForExit(string input)
        {
            if (input.ToUpper() == _EXIT)
            {
                System.Environment.Exit(0);
            }
        }
        #endregion
    }
}
