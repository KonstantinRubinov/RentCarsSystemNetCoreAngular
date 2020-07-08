using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class CarForRentModel
	{
		private int _rentNumber;
		private string _userID;
		private string _carNumber;
		private DateTime _rentStartDate;
		private DateTime _rentEndDate;
		private DateTime? _rentRealEndDate;

		[DataMember]
		public int rentNumber
		{
			get { return _rentNumber; }
			set { _rentNumber = value; }
		}

		[DataMember]
		//[Required(ErrorMessage = "Missing user id.")]
		//[PossibleID]
		public string userID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car number.")]
		//[StringLength(40, ErrorMessage = "Car number can't exceeds 40 chars.")]
		//[MinLength(9, ErrorMessage = "Car number mast be minimum 9 chars.")]
		public string carNumber
		{
			get { return _carNumber; }
			set { _carNumber = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing start date.")]
		public DateTime rentStartDate
		{
			get { return _rentStartDate; }
			set { _rentStartDate = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing end date.")]
		public DateTime rentEndDate
		{
			get { return _rentEndDate; }
			set { _rentEndDate = value; }
		}

		[DataMember]
		public DateTime? rentRealEndDate
		{
			get { return _rentRealEndDate; }
			set { _rentRealEndDate = value; }
		}

		public override string ToString()
		{
			return
				rentStartDate + " " +
				rentEndDate + " " +
				rentRealEndDate + " " +
				userID + " " +
				carNumber + " " +
				rentNumber;
		}

		public static CarForRentModel ToObject(DataRow reader)
		{
			DateTime dateValue;

			CarForRentModel carForRentModel = new CarForRentModel();
			carForRentModel.rentStartDate = DateTime.Parse(reader[0].ToString());
			carForRentModel.rentEndDate = DateTime.Parse(reader[1].ToString());


			if (DateTime.TryParse(reader[2].ToString(), out dateValue))
			{
				carForRentModel.rentRealEndDate = DateTime.Parse(reader[2].ToString());
			}
			else
			{
				carForRentModel.rentRealEndDate = null;
			}

			carForRentModel.userID = reader[3].ToString();
			carForRentModel.carNumber = reader[4].ToString();
			carForRentModel.rentNumber = int.Parse(reader[5].ToString());

			Debug.WriteLine("carForRentModel: " + carForRentModel.ToString());
			return carForRentModel;
		}
	}
}
