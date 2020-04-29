using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class Customer
    {
		#region Properties
		[Key]
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
		internal Customer()
		{ }
        #endregion

        #region Methods
        #endregion
    }
}
