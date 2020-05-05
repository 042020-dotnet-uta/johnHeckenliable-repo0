using System;

namespace CodingChallengeWeek3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Continue looping until they choose exit
            do
            {
                DisplayMenu();
                var selection = ValidateMainMenuSelection();
                ProcessMainMenuInput(selection);
            } while (true);
        }
        /// <summary>
        /// Will clear the console then display the 4 elements of the main menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Is the number even?");
            Console.WriteLine("2. Multiplication table");
            Console.WriteLine("3. Alternating Elements");
            Console.WriteLine("4. EXIT");
        }
        /// <summary>
        /// Will start the appropriate selection
        /// </summary>
        static void ProcessMainMenuInput(int selection)
        {
            switch (selection)
            {
                case 1:
                    PlayIsItEven();
                    break;
                case 2:
                    PlayMultiplcationTable();
                    break;
                case 3:
                    PlayShuffle();
                    break;
                case 4:
                    System.Environment.Exit(0);
                    break;
            }
        }
        /// <summary>
        /// Will validate that the user entered a number between 1 and 4
        /// </summary>
        /// <returns></returns>
        private static int ValidateMainMenuSelection()
        {
            var selection = 0;
            while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection >= 1 && selection <= 4))
            {
                Console.WriteLine("Please enter 1 - 4.");
            };
            return selection;
        }
        /// <summary>
        /// Starts the Is it Even
        /// </summary>
        static void PlayIsItEven()
        {
            //Clear the console
            Console.Clear();
            //Give the user destructions on expected input.
            Console.Write("Input an integer to check if it is even. ");
            var output = IsEven(Console.ReadLine());

            DisplayOutput(output);
            PressAnyKey();
        }
        /// <summary>
        /// Starts the multiplication Table
        /// Will check that the user inputs an int and loop until they do
        /// </summary>
        static void PlayMultiplcationTable()
        {
            //Clear the console
            Console.Clear();
            int num;
            //Continue to ask the user for an integer until they enter one
            do
            {
                Console.WriteLine("Enter an integer to get the multiplication table.");
            } while (!(int.TryParse(Console.ReadLine(), out num)));

            var output = MultTable(num);

            DisplayOutput(output);
            PressAnyKey();
        }
        /// <summary>
        /// Starts the Alternating Elements
        /// It will ask for 2 sets of 5 comma delimited values
        /// It will lopp until the user inputs them
        /// </summary>
        static void PlayShuffle()
        {
            //Clear the console
            Console.Clear();
            //Give teh user destructions on the expected input
            Console.Write("You will be asked to input 2 sets of 5 items each seperated by a comma(,). Plress any key to continue...");
            Console.ReadLine();
            string[] listA;
            do
            {
                Console.WriteLine("Enter the first set of 5 comma seperated values.");
                listA = Console.ReadLine().Split(',');
            } while (listA.Length != 5);
            string[] listB;
            do
            {
                Console.WriteLine("Enter the second set of 5 comma seperated values.");
                listB = Console.ReadLine().Split(',');
            } while (listB.Length != 5);

            var output = Shuffle(listA, listB);

            DisplayOutput(output);
            PressAnyKey();
        }
        /// <summary>
        /// Waits for the user to press any kety to return to the main menu
        /// </summary>
        static void PressAnyKey()
        {
            Console.Write("Press any key to return to the main menu.");
            Console.ReadLine();
        }
        /// <summary>
        /// Will display the output with a new line added
        /// </summary>
        /// <param name="output"></param>
        static void DisplayOutput(string output)
        {
            Console.WriteLine("\n" + output);
        }

        /// <summary>
        /// Will check the input for an even number
        /// </summary>
        /// <param name="input">the user inout to check for an even number</param>
        /// <returns></returns>
        public static string IsEven(string input)
        {
            var output = string.Empty;
            int num;

            //Try to parse the input into an integer
            if (int.TryParse(input, out num))//The input is an int
            {
                //Check if the number is even
                if ((num % 2) == 0)
                    output = "That number is even";
                else
                    output = $"{input} is not an even number";
            }
            else//The inpout is not an int
            {
                output = $"{input} is not a number";
            }
            //Return the formatted text
            return output;
        }
        /// <summary>
        /// Will create a display a multiplcation table for the number provided
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string MultTable(int num)
        {
            var output = string.Empty;
            //Loop through twice to add all possible multiplications
            //i will iterate through the first number and j will iterate through the second number
            for (int i = 1; i <= num; i++)
            {
                for (int j = 1; j <= num; j++)
                {
                    output += $"{i} x {j} = {i * j}, ";
                }
            }
            //Trim the excess space and , from the end of the srting
            output = output.TrimEnd().TrimEnd(',');
            //Return the formatted text
            return output;
        }
        /// <summary>
        /// Will create and display a string containing the 2 sets alternated
        /// </summary>
        /// <param name="a">the first set of 5 values</param>
        /// <param name="b">the second set of 5 values</param>
        /// <returns></returns>
        public static string Shuffle(string[] a, string[] b)
        {
            var output = string.Empty;
            //Loop through the length of the arrays
            for (int i = 0; i < a.Length; i++)
            {
                //add the value at i from both arrays to the output string
                output += $"{a[i]},{b[i]},";
            }
            output = output.TrimEnd(',');
            return output;
        }
    }
}
