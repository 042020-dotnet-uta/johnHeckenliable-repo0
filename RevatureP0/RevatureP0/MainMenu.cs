using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class MainMenu
    {
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
    }
}