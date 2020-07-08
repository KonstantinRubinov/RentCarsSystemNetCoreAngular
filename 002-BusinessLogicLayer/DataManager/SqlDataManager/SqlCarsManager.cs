using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlCarsManager : SqlDataBase, ICarsRepository
	{
		public List<CarModel> GetAllCars()
		{
			DataTable dt = new DataTable();
			List<CarModel> arrCars = new List<CarModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.GetAllCars());
			}

			foreach (DataRow ms in dt.Rows)
			{
				CarModel carModel = CarModel.ToObject(ms);

				arrCars.Add(carModel);
			}

			return arrCars;
		}

		public CarModel GetOneCar(string carNumber)
		{
			if (carNumber.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			CarModel carModelSql = new CarModel();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.GetOneCarByNumber(carNumber));
			}

			foreach (DataRow ms in dt.Rows)
			{
				carModelSql = CarModel.ToObject(ms);
			}

			return carModelSql;
		}







		public CarModel AddCar(CarModel carModel)
		{
			DataTable dt = new DataTable();
			CarModel carModelSql = new CarModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.AddCar(carModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carModelSql = CarModel.ToObject(ms);
			}

			return carModelSql;
		}

		public CarModel UpdateCar(CarModel carModel)
		{
			DataTable dt = new DataTable();
			CarModel carModelSql = new CarModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.UpdateCar(carModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carModelSql = CarModel.ToObject(ms);
			}

			return carModelSql;
		}

		public string DeleteCar(string carNumber)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(CarStringsSql.DeleteCar(carNumber));
			}
			return i.ToString();
		}

		public CarModel UploadCarImage(string carNumber, string img)
		{
			DataTable dt = new DataTable();
			CarModel carModel = GetOneCar(carNumber);
			CarModel carModelSql = new CarModel();
			if (carModel == null)
				return carModel;
			carModel.carPicture = img;

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.UpdateCar(carModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carModelSql = CarModel.ToObject(ms);
			}

			return carModelSql;
		}


		public List<CarPictureModel> GetAllCarImagesAndNumberOfCars()
		{
			DataTable dt = new DataTable();
			List<CarPictureModel> arrCarPictures = new List<CarPictureModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.GetAllCarImagesAndNumberOfCars());
			}

			foreach (DataRow ms in dt.Rows)
			{
				CarPictureModel carPictureModel = CarPictureModel.ToObject(ms);

				arrCarPictures.Add(carPictureModel);
			}

			return arrCarPictures;
		}


		public CarPictureModel GetNumberOfCarWithImage(string pictureName)
		{
			if (pictureName.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			CarPictureModel carPictureModelSql = new CarPictureModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarStringsSql.GetNumberOfCarWithImage(pictureName));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carPictureModelSql = CarPictureModel.ToObject(ms);
			}

			return carPictureModelSql;
		}
	}
}
