using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    public class AdminUI : IInterface
    {
        public bool Run()
        {
            return false;
        }
        private void PrintMainMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. Customer Information.");
            Console.WriteLine("2. Location Information.");
        }

        private void PrintTitle()
        {
            Console.WriteLine("******Admistration Portal******");
        }
    }
}
