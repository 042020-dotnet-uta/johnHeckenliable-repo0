using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class CustomerUI : IInterface
    {
        public void PrintMainMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. Login.");
            Console.WriteLine("2. Create new Account.");
        }

        public void PrintTitle()
        {
            Console.WriteLine("Choose something.\n");
        }
    }
}
