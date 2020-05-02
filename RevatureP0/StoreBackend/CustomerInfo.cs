using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreBackend_Api
{
    public class CustomerInfo
    {
		#region Properties
		public int CustomerId { get; set; }

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
		public CustomerInfo()
		{ }

		public CustomerInfo(int ID, string fName, string lName, string phoneNum)
		{
			this.CustomerId = ID;
			this.FirstName = fName;
			this.LastName = lName;
			this.PhoneNumber = phoneNum;
		}
        #endregion

        #region Methods
        #endregion
    }
}
