using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    public class AdminUI : IInterface
    {
        StoreBackend db;
        public AdminUI() { db = new StoreBackend(); }
        public bool Run()
        {
            bool contnue = false;
            do
            {
                PrintMainMenu();
                int selection;
                while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection == 1 || selection == 2))
                {
                    Console.WriteLine("Please enter 1 or 2.");
                };
                switch (selection)
                {
                    case 1:
                        contnue = ManageCustomerSection();
                        break;
                    case 2:
                        contnue = ManageLocationSection();
                        break;
                    default:
                        break;
                }
            } while (!contnue);

            return true;
        }

        private bool ManageLocationSection()
        {
            var locations = db.GetAllLocations();
            foreach (var location in locations)
            {
                Console.WriteLine($"{location.StoreId}. {location.Location}");
            }
            
            int selection;
            do
            {
                Console.Write("Select a store number to view available actions. ");
            } while (int.TryParse(Console.ReadLine(), out selection) && !ValidLocation(selection, locations));
        }
        private bool ValidLocation(int id, List<LocationInfo> locations)
        {
            foreach (var loc in locations)
            {
                if (loc.StoreId == id)
                    return true;
            }
            return false;
        }
        private void PrintLocationMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. View product inventories..");
            Console.WriteLine("2. View order history..");
        }

        private bool ManageCustomerSection()
        {
            throw new NotImplementedException();
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
