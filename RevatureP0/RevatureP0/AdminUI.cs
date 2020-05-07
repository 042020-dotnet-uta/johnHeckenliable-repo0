using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using P0DatabaseApi;
using System.ComponentModel.Design;

namespace RevatureP0
{
    public class AdminUI : IInterface
    {
        StoreBackend db;
        public AdminUI() { db = new StoreBackend(); }
        public bool Run()
        {
            //bool contnue = false;
            int selection;
            do
            {
                PrintMainMenu();
                while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection < 1 && selection > 3))
                {
                    Console.WriteLine("Please enter 1 or 2.");
                };
                switch (selection)
                {
                    case 1:
                       // ManageCustomerSection();
                        break;
                    case 2:
                        ManageLocationSection();
                        break;
                    default:
                        break;
                }
            } while (selection != 3);

            return true;
        }

        private void ManageLocationSection()
        {
            var locations = db.GetAllLocations();
            //bool toContinue = true;
            //do
            //{
                int storeID = ManageLocations(locations);

                PrintLocationMenu();
                int selection;
                while ((!int.TryParse(Console.ReadLine(), out selection)) && (selection == 1 || selection ==2 ))
                {
                    Console.WriteLine("Please enter a 1 to view product Inventory or 2 to view order history.");
                };
                switch (selection)
                {
                    case 1:
                        DisplayProductInventory(storeID, locations);
                        break;
                    case 2:
                        DisplayOrderHistory(storeID);
                        break;
                    default:
                       // toContinue = false;
                        break;
                }

            //} while (toContinue);
            //return toContinue;
        }

        private int ManageLocations(List<LocationInfo> locations)
        {
            //bool okToContinue = false;
            //do
            //{
                Console.Clear();
                foreach (var location in locations)
                {
                    Console.WriteLine($"{location.StoreId}. {location.Location}");
                }
                
                int selection;
                do
                {
                    Console.Write("Select a store number to view available actions. ");
                } while (int.TryParse(Console.ReadLine(), out selection) && !ValidLocation(selection, locations));

            return selection;
            //} while (!okToContinue);
            
            //return okToContinue;
        }
        private void DisplayProductInventory(int storeId, List<LocationInfo> locs)
        {
            var location = (from loc in locs
                            where loc.StoreId == storeId
                            select loc).FirstOrDefault();

            Console.Clear();
            Console.WriteLine($"\n{location.StoreId} {location.Location}");
            foreach (var prod in location.AvailableProducts)
            {
                Console.WriteLine($"\t{prod.ProductId}. {prod.ProductDescription} available quantity {prod.Quantity}");
            }
            Console.Write("\n\nPress any key to return to the previous menu.");
            Console.ReadLine();
        }
        private void DisplayOrderHistory(int storeID)
        {

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
            Console.WriteLine("2. View order history.");
            //Console.WriteLine("3. Return to main menu.");
        }

        private void ManageCustomerSection()
        {
            //throw new NotImplementedException();
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            PrintTitle();

            Console.WriteLine("1. Customer Information.");
            Console.WriteLine("2. Location Information.");
            Console.WriteLine("3. Return to main menu.");
        }

        private void PrintTitle()
        {
            //Console.WriteLine("******Admistration Portal******");
        }
    }
}
