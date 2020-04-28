using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    internal class Product
    {
        #region Properties
        public int PoductId { get; set; }

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
        {  }
        #endregion

        #region Methods

        #endregion
    }
}