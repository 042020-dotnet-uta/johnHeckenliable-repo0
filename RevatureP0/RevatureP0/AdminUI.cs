﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    public class AdminUI : IInterface
    {
        public void PrintMainMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. Customer Information.");
            Console.WriteLine("2. Location Information.");
        }

        public void PrintTitle()
        {
            Console.WriteLine("******Admistration Portal******");
        }
    }
}
