using System;

namespace RevatureP0
{
    class Program
    {
        const string _EXIT = "EXIT";
        static void Main(string[] args)
        {
            //Welcome the user
            Console.WriteLine("***Welcome to THE store***");
            //Ask the user to 1. Log in 2. create an account
            var selection = GetUserLoginSelction();

                //1. verify the account
                //2. Create the user
                //prompt user if there is an issue with account/creation
                //Go back to log in or creation???

            //Ask the user to select a store location (menu of n choices)

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