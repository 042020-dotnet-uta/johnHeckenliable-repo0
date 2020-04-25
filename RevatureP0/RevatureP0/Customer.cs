using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    internal class Customer
    {
        #region Properties
        private int customerID;
		public int CustomerID { get { return customerID; } }

		private string firstName;
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		private string lastName;
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		private string phoneNumber;
		public string PhoneNumber
		{
			get { return phoneNumber; }
			set { phoneNumber = value; }
		}
        #endregion

        #region Constructors
		internal Customer()
		{
			//Create a new ID for them???
		}
		internal Customer(int customerID)
		{
			//Load existing costomer data???
		}
        #endregion

        #region Methods
		public string GetFullName()
		{
			return $"{FirstName} {LastName}";
		}
        #endregion
    }
}
