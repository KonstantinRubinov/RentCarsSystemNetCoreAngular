using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlCarForRentManager : MySqlDataBase, ICarForRentRepository
	{
		public List<CarForRentModel> GetAllCarsForRent()
		{
			DataTable dt = new DataTable();
			List<CarForRentModel> arrCarsForRent = new List<CarForRentModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.GetAllCarsForRent());
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrCarsForRent.Add(CarForRentModel.ToObject(ms));
			}

			return arrCarsForRent;
		}

		public List<CarForRentModel> GetCarsForRentByCarNumber(string carNumber)
		{
			if (carNumber.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			List<CarForRentModel> arrCarsForRent = new List<CarForRentModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.GetAllCarsForRentByCarNumber(carNumber));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrCarsForRent.Add(CarForRentModel.ToObject(ms));
			}

			return arrCarsForRent;
		}


		public List<FullCarDataModel> GetCarsForRentByUserId(string userID)
		{
			if (userID.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			List<FullCarDataModel> arrCarsForRent = new List<FullCarDataModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.GetAllCarsForRentByUserId(userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrCarsForRent.Add(FullCarDataModel.ToObject(ms));
			}

			return arrCarsForRent;
		}


		public CarForRentModel GetOneCarForRentByRentNumber(int rentNumber)
		{
			if (rentNumber < 0)
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			CarForRentModel carForRentModelSql = new CarForRentModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.GetOneCarForRentByRentNumber(rentNumber));
			}

			foreach (DataRow ms in dt.Rows)
			{
				carForRentModelSql = CarForRentModel.ToObject(ms);
			}

			return carForRentModelSql;
		}

		public CarForRentModel AddCarForRent(CarForRentModel carForRentModel)
		{
			DataTable dt = new DataTable();
			CarForRentModel carForRentModelSql = new CarForRentModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.AddCarForRent(carForRentModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				carForRentModelSql = CarForRentModel.ToObject(ms);
			}

			return carForRentModelSql;
		}

		public CarForRentModel UpdateCarForRent(CarForRentModel carForRentModel)
		{
			string id = "";
			//string id = HttpContext.Current.User.Identity.Name;
			carForRentModel.userID = id;

			DataTable dt = new DataTable();
			CarForRentModel carForRentModelSql = new CarForRentModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsMySql.UpdateCarForRent(carForRentModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				carForRentModelSql = CarForRentModel.ToObject(ms);
			}

			return carForRentModelSql;
		}

		public int DeleteCarForRent(int rentNumber)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(CarForRentStringsMySql.DeleteCarForRentByRent(rentNumber));
			}
			return i;
		}

		public int DeleteCarForRent(string carNumber)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(CarForRentStringsMySql.DeleteCarForRentByCar(carNumber));
			}
			return i;
		}
	}
}
