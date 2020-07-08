using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlCarsManager : MySqlDataBase, ICarsRepository
	{
		public List<CarModel> GetAllCars()
		{
			DataTable dt = new DataTable();
			List<CarModel> arrCars = new List<CarModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.GetAllCars());
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

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.GetOneCarByNumber(carNumber));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.AddCar(carModel));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.UpdateCar(carModel));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(CarStringsMySql.DeleteCar(carNumber));
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

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.UpdateCar(carModel));
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

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.GetAllCarImagesAndNumberOfCars());
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarStringsMySql.GetNumberOfCarWithImage(pictureName));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carPictureModelSql = CarPictureModel.ToObject(ms);
			}

			return carPictureModelSql;
		}
	}
}
