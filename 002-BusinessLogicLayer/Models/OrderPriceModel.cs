using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class OrderPriceModel
	{
		private string _carNumber;
		private DateTime _rentStartDate;
		private DateTime _rentEndDate;
		private decimal _carPrice;
		private int _orderDays;

		[DataMember]
		[Required(ErrorMessage = "Missing car number.")]
		[StringLength(40, ErrorMessage = "Car number can't exceeds 40 chars.")]
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
		public decimal carPrice
		{
			get { return _carPrice; }
			set { _carPrice = value; }
		}

		[DataMember]
		public int orderDays
		{
			get { return _orderDays; }
			set { _orderDays = value; }
		}

		public override string ToString()
		{
			return
				carNumber + " " +
				rentStartDate + " " +
				rentEndDate + " " +
				carPrice + " " +
				orderDays;
		}

		public static OrderPriceModel ToObject(SqlDataReader reader)
		{
			DateTime dateValue;

			OrderPriceModel orderPriceModel = new OrderPriceModel();
			orderPriceModel.carNumber = reader[0].ToString();
			if (DateTime.TryParse(reader[1].ToString(), out dateValue))
			{
				orderPriceModel.rentStartDate = DateTime.Parse(reader[1].ToString());
			}

			if (DateTime.TryParse(reader[2].ToString(), out dateValue))
			{
				orderPriceModel.rentEndDate = DateTime.Parse(reader[2].ToString());
			}
			orderPriceModel.carPrice = decimal.Parse(reader[3].ToString());
			orderPriceModel.orderDays = int.Parse(reader[4].ToString());

			Debug.WriteLine("orderPriceModel: " + orderPriceModel.ToString());
			return orderPriceModel;
		}
	}
}
