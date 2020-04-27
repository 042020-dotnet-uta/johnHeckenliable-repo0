using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge04._27._20
{
    class SweetNSalty
    {
        internal void Run()
        {
            //Declare and initialize to 0 the counters for each category
            var sweetCount = 0;
            var saltyCount = 0;
            var ssCount = 0;

            //Loop through numbers 1-100
            for (int i = 1; i <= 100; i++)
            {
                //Declare the variable to hold the output and set to an empty string
                var output = string.Empty;
                //is i a multiple of both 3 and 5?
                if (IsMultipleOf3(i) && IsMultipleOf5(i))
                {
                    output = "Sweet'nSalty";
                    ssCount++;
                }
                //is i a multiple of 3 (but not 5)
                else if (IsMultipleOf3(i))
                {
                    output = "Sweet";
                    sweetCount++;
                }
                //is i a multiple of 5 (but not 3)
                else if (IsMultipleOf5(i))
                {
                    output = "Salty";
                    saltyCount++;
                }
                //i is not a multiple of either 3 or 5
                else
                    output = i.ToString();

                //Display the output
                Console.WriteLine(output);
            }
        //Display the count for each category
        Console.WriteLine($"\nThere were {sweetCount} Sweets, {saltyCount} Saltys, and {ssCount} SweetnSaltys");
        }
        private bool IsMultipleOf3(int i)
        {
            //return T/F of i mod 3 == 0
            return i % 3 == 0;
        }
        private bool IsMultipleOf5(int i)
        {
            //return T/F of i mod 5 == 0
            return i % 5 == 0;
        }
    }
}