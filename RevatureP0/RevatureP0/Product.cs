using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    internal class Product
    {
        #region Properties
        private int productID;
        public int ProductID
        {
            get { return productID; }
            //set { productID = value; }
        }
        private string productDesc;
        public string ProductDescription
        {
            get { return productDesc; }
            set { productDesc = value; }
        }
        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        #endregion

        #region Constructors
        internal Product()
        {
            //generate a productID???

        }
        internal Product(int productID)
        {
            this.productID = productID;
        }
        #endregion

        #region Methods

        #endregion
    }
}