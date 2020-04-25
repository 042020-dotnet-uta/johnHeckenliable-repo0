using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class Controller
    {
        internal string[] GetStoreList()
        {
            return new string[]{
                "Store Location A",
                "Store Location B",
                "Store Location C"
            };
        }
        internal Store GetStoreInfo(int storeID)
        {
            return new Store(storeID);
        }
        internal Customer CreatUserAccount()
        {
            return new Customer();
        }
        internal Customer GetCustomer(string name)
        {
            return new Customer(name);
        }
    }
}
