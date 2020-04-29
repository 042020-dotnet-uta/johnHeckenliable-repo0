using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class Product
    {
        #region Properties
        [Key]
        public int PoductId { get; set; }

        public string ProductDescription { get; set; }

        public double Price { get; set; }

        #endregion

        #region Constructors
        internal Product()
        {  }
        #endregion

        #region Methods

        #endregion
    }
}