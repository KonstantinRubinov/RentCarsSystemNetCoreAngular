using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class CarTypeModel
	{
		private string _carTypeIdMongo;

		private int _carTypeId;
		private string _carType;
		private string _carFirm;
		private string _carModel;
		private decimal _carDayPrice;
		private decimal _carLatePrice;
		private int _carYear;
		private string _carGear;

		public CarTypeModel(int tmpCarTypeId, string tmpCarType, string tmpCarFirm, string tmpCarModel, decimal tmpCarDayPrice, decimal tmpCarLatePrice, int tmpCarYear, string tmpCarGear)
		{
			carTypeId = tmpCarTypeId;
			carType = tmpCarType;
			carFirm = tmpCarFirm;
			carModel = tmpCarModel;
			carDayPrice = tmpCarDayPrice;
			carLatePrice = tmpCarLatePrice;
			carYear = tmpCarYear;
			carGear = tmpCarGear;
		}

		public CarTypeModel(string tmpCarTypeIdMongo, string tmpCarType, string tmpCarFirm, string tmpCarModel, decimal tmpCarDayPrice, decimal tmpCarLatePrice, int tmpCarYear, string tmpCarGear)
		{
			carTypeIdMongo = tmpCarTypeIdMongo;
			carType = tmpCarType;
			carFirm = tmpCarFirm;
			carModel = tmpCarModel;
			carDayPrice = tmpCarDayPrice;
			carLatePrice = tmpCarLatePrice;
			carYear = tmpCarYear;
			carGear = tmpCarGear;
		}

		public CarTypeModel(string tmpCarType, string tmpCarFirm, string tmpCarModel, decimal tmpCarDayPrice, decimal tmpCarLatePrice, int tmpCarYear, string tmpCarGear)
		{
			carType = tmpCarType;
			carFirm = tmpCarFirm;
			carModel = tmpCarModel;
			carDayPrice = tmpCarDayPrice;
			carLatePrice = tmpCarLatePrice;
			carYear = tmpCarYear;
			carGear = tmpCarGear;
		}

		public CarTypeModel()
		{

		}

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string carTypeIdMongo
		{
			get { return _carTypeIdMongo; }
			set { _carTypeIdMongo = value; }
		}

		[DataMember]
		public int carTypeId
		{
			get { return _carTypeId; }
			set { _carTypeId = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car type.")]
		public string carType
		{
			get { return _carType; }
			set { _carType = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car firm.")]
		[StringLength(40, ErrorMessage = "Car firm can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "Car firm mast be minimum 2 chars.")]
		public string carFirm
		{
			get { return _carFirm; }
			set { _carFirm = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car model.")]
		[StringLength(40, ErrorMessage = "Car model can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "Car model mast be minimum 2 chars.")]
		public string carModel
		{
			get { return _carModel; }
			set { _carModel = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car Day Price.")]
		public decimal carDayPrice
		{
			get { return _carDayPrice; }
			set { _carDayPrice = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car Late Price.")]
		public decimal carLatePrice
		{
			get { return _carLatePrice; }
			set { _carLatePrice = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing car Date.")]
		public int carYear
		{
			get { return _carYear; }
			set { _carYear = value; }
		}


		[DataMember]
		[Required(ErrorMessage = "Missing car Gere.")]
		public string carGear
		{
			get { return _carGear; }
			set { _carGear = value; }
		}

		public override string ToString()
		{
			return
				carType + " " +
				carFirm + " " +
				carModel + " " +
				carDayPrice + " " +
				carLatePrice + " " +
				carYear + " " +
				carGear + " " +
				carTypeId + " " +
				carTypeIdMongo;
		}

		public static CarTypeModel ToObject(DataRow reader)
		{
			CarTypeModel carTypeModel = new CarTypeModel();
			carTypeModel.carType = reader[0].ToString();
			carTypeModel.carFirm = reader[1].ToString();
			carTypeModel.carModel = reader[2].ToString();
			carTypeModel.carDayPrice = decimal.Parse(reader[3].ToString());
			carTypeModel.carLatePrice = decimal.Parse(reader[4].ToString());
			carTypeModel.carYear = int.Parse(reader[5].ToString());
			carTypeModel.carGear = reader[6].ToString();
			carTypeModel.carTypeId = int.Parse(reader[7].ToString());

			Debug.WriteLine("carTypeModel: " + carTypeModel.ToString());
			return carTypeModel;
		}

		public static CarTypeModel ToObjectTyepId(DataRow reader)
		{
			CarTypeModel carTypeModel = new CarTypeModel();
			carTypeModel.carTypeId = int.Parse(reader[0].ToString());
			carTypeModel.carType = reader[1].ToString();

			Debug.WriteLine("carTypeModel: " + carTypeModel.ToString());
			return carTypeModel;
		}
	}
}
