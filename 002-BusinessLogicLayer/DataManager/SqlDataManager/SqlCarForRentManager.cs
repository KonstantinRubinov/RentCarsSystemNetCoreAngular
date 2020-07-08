using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlCarForRentManager : SqlDataBase, ICarForRentRepository
	{
		public List<CarForRentModel> GetAllCarsForRent()
		{
			DataTable dt = new DataTable();
			List<CarForRentModel> arrCarsForRent = new List<CarForRentModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.GetAllCarsForRent());
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.GetAllCarsForRentByCarNumber(carNumber));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.GetAllCarsForRentByUserId(userID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.GetOneCarForRentByRentNumber(rentNumber));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.AddCarForRent(carForRentModel));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarForRentStringsSql.UpdateCarForRent(carForRentModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(CarForRentStringsSql.DeleteCarForRentByRent(rentNumber));
			}
			return i;
		}

		public int DeleteCarForRent(string carNumber)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(CarForRentStringsSql.DeleteCarForRentByCar(carNumber));
			}
			return i;
		}
	}
}
