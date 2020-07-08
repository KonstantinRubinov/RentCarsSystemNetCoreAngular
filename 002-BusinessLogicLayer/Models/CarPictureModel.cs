using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class CarPictureModel
	{
		private string _carPictureLink;
		private string _carPictureName;
		private int _numberOfCars;

		[DataMember]
		public string carPictureLink
		{
			get { return _carPictureLink; }
			set { _carPictureLink = value; }
		}

		[DataMember]
		public string carPictureName
		{
			get { return _carPictureName; }
			set { _carPictureName = value; }
		}

		[DataMember]
		public int numberOfCars
		{
			get { return _numberOfCars; }
			set { _numberOfCars = value; }
		}

		public override string ToString()
		{
			return
				carPictureLink + " " +
				carPictureName + " " +
				numberOfCars;
		}

		public static CarPictureModel ToObject(DataRow reader)
		{
			CarPictureModel carPictureModel = new CarPictureModel();
			carPictureModel.carPictureLink = reader[0].ToString();

			if (!carPictureModel.carPictureLink.Equals(string.Empty) && !carPictureModel.carPictureLink.Equals(""))
			{
				carPictureModel.carPictureLink = "/assets/images/cars/" + carPictureModel.carPictureLink;
			}


			carPictureModel.carPictureName = reader[0].ToString();
			carPictureModel.numberOfCars = int.Parse(reader[1].ToString());

			Debug.WriteLine("carPictureModel: " + carPictureModel.ToString());
			return carPictureModel;
		}
	}
}
