using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    public static class utils
    {
        readonly static string _EXIT = "EXIT";
        static readonly string _RETURN = "RETURN";
        public static void CheckForExit(string input)
        {
            if (input.ToUpper() == _EXIT)
            {
                //Close the application
                System.Environment.Exit(0);
            }
        }
        public static void CheckForReturn(string input)
        {
            if (input.ToUpper() == _RETURN)
            {
                //Load up the min menu
            }
        }
        public static string ProcessInput()
        {
            var input = Console.ReadLine();
            utils.CheckForExit(input);
            utils.CheckForReturn(input);
            return input;
        }
    }
}
