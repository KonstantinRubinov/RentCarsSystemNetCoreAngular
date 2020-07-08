using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class CarModel
	{
		private string _carNumber;
		private double _carKm;
		private string _carPicture;
		private bool _carInShape;
		private bool _carAvaliable = true;
		private int _carTypeID;
		private int _carBranchID;
		private string _carTypeIDMongo;
		private string _carBranchIDMongo;

		public CarModel(double tmpCarKm, string tmpCarPicture, bool tmpCarInShape, bool tmpCarAvaliable, string tmpCarNumber, string tmpCarBranchIDMongo, string tmpCarTypeIDMongo)
		{
			carNumber = tmpCarNumber;
			carKm = tmpCarKm;
			carPicture = tmpCarPicture;
			carInShape = tmpCarInShape;
			carAvaliable = tmpCarAvaliable;
			carTypeIDMongo = tmpCarBranchIDMongo;
			carBranchIDMongo = tmpCarTypeIDMongo;
		}

		public CarModel(double tmpCarKm, string tmpCarPicture, bool tmpCarInShape, bool tmpCarAvaliable, string tmpCarNumber)
		{
			carNumber = tmpCarNumber;
			carKm = tmpCarKm;
			carPicture = tmpCarPicture;
			carInShape = tmpCarInShape;
			carAvaliable = tmpCarAvaliable;
		}

		public CarModel()
		{

		}

		[BsonId]
		[DataMember]
		[Required(ErrorMessage = "Missing car number.")]
		[StringLength(40, ErrorMessage = "Car number can't exceeds 40 chars.")]
		[MinLength(9, ErrorMessage = "Car number mast be minimum 9 chars.")]
		public string carNumber
		{
			get { return _carNumber; }
			set { _carNumber = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car km.")]
		public double carKm
		{
			get { return _carKm; }
			set { _carKm = value; }
		}

		[DataMember]
		public string carPicture
		{
			get { return _carPicture; }
			set { _carPicture = value; }
		}

		[DataMember]
		public bool carInShape
		{
			get { return _carInShape; }
			set { _carInShape = value; }
		}

		[DataMember]
		public bool carAvaliable
		{
			get { return _carAvaliable; }
			set { _carAvaliable = value; }
		}

		[DataMember]
		public int carTypeID
		{
			get { return _carTypeID; }
			set { _carTypeID = value; }
		}

		[DataMember]
		public int carBranchID
		{
			get { return _carBranchID; }
			set { _carBranchID = value; }
		}



		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string carTypeIDMongo
		{
			get { return _carTypeIDMongo; }
			set { _carTypeIDMongo = value; }
		}

		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string carBranchIDMongo
		{
			get { return _carBranchIDMongo; }
			set { _carBranchIDMongo = value; }
		}





		public override string ToString()
		{
			return
				carKm + " " +
				carPicture + " " +
				carInShape + " " +
				carAvaliable + " " +
				carNumber + " " +
				carBranchID + " " +
				carTypeID;
		}

		public static CarModel ToObject(DataRow reader)
		{
			CarModel carModel = new CarModel();
			carModel.carKm = double.Parse(reader[0].ToString());
			carModel.carPicture = reader[1].ToString();
			if (!carModel.carPicture.Equals(string.Empty) && !carModel.carPicture.Equals(""))
			{
				carModel.carPicture = "/assets/images/cars/" + carModel.carPicture;
			}

			try
			{
				carModel.carInShape = int.Parse(reader[2].ToString()) > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql carInShape: " + ex.Message);
			}

			try
			{
				carModel.carInShape = bool.Parse(reader[2].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql carInShape: " + ex.Message);
			}


			try
			{
				carModel.carAvaliable = int.Parse(reader[3].ToString()) > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql carAvaliable: " + ex.Message);
			}

			try
			{
				carModel.carAvaliable = bool.Parse(reader[3].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql carAvaliable: " + ex.Message);
			}
			
			carModel.carNumber = reader[4].ToString();
			carModel.carBranchID = int.Parse(reader[5].ToString());
			carModel.carTypeID = int.Parse(reader[6].ToString());

			Debug.WriteLine("carModel: " + carModel.ToString());
			return carModel;
		}
	}
}
