using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class FullCarDataModel
	{
		private string _carNumber;
		private string _carType;
		private double _carKm;
		private string _carPicture;
		private bool _carInShape;
		private bool _carAvaliable = false;
		private int _carBranchID;

		private string _carBranchIDMongo;

		[DataMember]
		public string carNumber
		{
			get { return _carNumber; }
			set { _carNumber = value; }
		}

		[DataMember]
		public string carType
		{
			get { return _carType; }
			set { _carType = value; }
		}

		[DataMember]
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
		public int carBranchID
		{
			get { return _carBranchID; }
			set { _carBranchID = value; }
		}



		[DataMember]
		public string carBranchIDMongo
		{
			get { return _carBranchIDMongo; }
			set { _carBranchIDMongo = value; }
		}


		private string _carFirm;
		private string _carModel;
		private decimal _carDayPrice;
		private decimal _carLatePrice;
		private int _carYear;
		private string _carGear;

		[DataMember]
		public string carFirm
		{
			get { return _carFirm; }
			set { _carFirm = value; }
		}

		[DataMember]
		public string carModel
		{
			get { return _carModel; }
			set { _carModel = value; }
		}

		[DataMember]
		public decimal carDayPrice
		{
			get { return _carDayPrice; }
			set { _carDayPrice = value; }
		}

		[DataMember]
		public decimal carLatePrice
		{
			get { return _carLatePrice; }
			set { _carLatePrice = value; }
		}

		[DataMember]
		public int carYear
		{
			get { return _carYear; }
			set { _carYear = value; }
		}

		[DataMember]
		public string carGear
		{
			get { return _carGear; }
			set { _carGear = value; }
		}




		private string _branchName;
		private string _branchAddress;
		private double _branchLat;
		private double _branchLng;



		[DataMember]
		public string branchName
		{
			get { return _branchName; }
			set { _branchName = value; }
		}

		[DataMember]
		public string branchAddress
		{
			get { return _branchAddress; }
			set { _branchAddress = value; }
		}

		[DataMember]
		public double branchLat
		{
			get { return _branchLat; }
			set { _branchLat = value; }
		}

		[DataMember]
		public double branchLng
		{
			get { return _branchLng; }
			set { _branchLng = value; }
		}

		private decimal _carPrice = 0;

		[DataMember]
		public decimal carPrice
		{
			get { return _carPrice; }
			set { _carPrice = value; }
		}

		private int _numerOfCars = 1;



		[DataMember]
		public int numerOfCars
		{
			get { return _numerOfCars; }
			set { _numerOfCars = value; }
		}
		public override string ToString()
		{
			return
				carNumber + " " +
				carKm + " " +
				carPicture + " " +
				carInShape + " " +
				carAvaliable + " " +
				carBranchID + " " +

				carType + " " +
				carFirm + " " +
				carModel + " " +
				carDayPrice + " " +
				carLatePrice + " " +
				carYear + " " +
				carGear + " " +

				branchName + " " +
				branchAddress + " " +
				branchLat + " " +
				branchLng + " " +
				numerOfCars;
		}

		public static FullCarDataModel ToObject(DataRow reader)
		{
			FullCarDataModel fullCarDataModel = new FullCarDataModel();

			fullCarDataModel.carNumber = reader[0].ToString();
			fullCarDataModel.carKm = double.Parse(reader[1].ToString());
			fullCarDataModel.carPicture = reader[2].ToString();
			if (!fullCarDataModel.carPicture.Equals(string.Empty) && !fullCarDataModel.carPicture.Equals(""))
			{
				fullCarDataModel.carPicture = "/assets/images/cars/" + fullCarDataModel.carPicture;
			}
			
			
			try
			{
				fullCarDataModel.carInShape = int.Parse(reader[3].ToString()) > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql carInShape: " + ex.Message);
			}

			try
			{
				fullCarDataModel.carInShape = bool.Parse(reader[3].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql carInShape: " + ex.Message);
			}


			try
			{
				fullCarDataModel.carAvaliable = int.Parse(reader[4].ToString()) > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql carAvaliable: " + ex.Message);
			}

			try
			{
				fullCarDataModel.carAvaliable = bool.Parse(reader[4].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql carAvaliable: " + ex.Message);
			}
			
			
			fullCarDataModel.carBranchID = int.Parse(reader[5].ToString());

			fullCarDataModel.carType = reader[6].ToString();
			fullCarDataModel.carFirm = reader[7].ToString();
			fullCarDataModel.carModel = reader[8].ToString();
			fullCarDataModel.carDayPrice = decimal.Parse(reader[9].ToString());
			fullCarDataModel.carLatePrice = decimal.Parse(reader[10].ToString());
			fullCarDataModel.carYear = int.Parse(reader[11].ToString());
			fullCarDataModel.carGear = reader[12].ToString();

			fullCarDataModel.branchName = reader[13].ToString();
			fullCarDataModel.branchAddress = reader[14].ToString();
			fullCarDataModel.branchLat = double.Parse(reader[15].ToString());
			fullCarDataModel.branchLng = double.Parse(reader[16].ToString());

			try
			{
				fullCarDataModel.numerOfCars = int.Parse(reader[17].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Number Of Cars Exception: " + ex.Message);
			}


			Debug.WriteLine("fullCarDataModel: " + fullCarDataModel.ToString());
			return fullCarDataModel;
		}
	}
}
