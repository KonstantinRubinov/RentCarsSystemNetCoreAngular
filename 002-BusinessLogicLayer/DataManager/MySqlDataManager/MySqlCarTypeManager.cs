using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlCarTypeManager : MySqlDataBase, ICarTypeRepository
	{
		public List<CarTypeModel> GetAllCarTypesIds()
		{
			DataTable dt = new DataTable();
			List<CarTypeModel> arrCarTypes = new List<CarTypeModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsMySql.GetAllCarTypesIds());
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrCarTypes.Add(CarTypeModel.ToObjectTyepId(ms));
			}

			return arrCarTypes;
		}


		public List<CarTypeModel> GetAllCarTypes()
		{
			DataTable dt = new DataTable();
			List<CarTypeModel> arrCarTypes = new List<CarTypeModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsMySql.GetAllCarTypes());
			}
			foreach (DataRow ms in dt.Rows)
			{
				arrCarTypes.Add(CarTypeModel.ToObject(ms));
			}

			return arrCarTypes;
		}

		public CarTypeModel GetOneCarType(int typeId)
		{
			if (typeId < 0)
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			CarTypeModel carTypeModelSql = new CarTypeModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsMySql.GetOneCarType(typeId));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carTypeModelSql = CarTypeModel.ToObject(ms);
			}

			return carTypeModelSql;
		}

		public CarTypeModel AddCarType(CarTypeModel carTypeModelSql)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsMySql.AddCarType(carTypeModelSql));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carTypeModelSql = CarTypeModel.ToObject(ms);
			}

			return carTypeModelSql;
		}

		public CarTypeModel UpdateCarType(CarTypeModel carTypeModelSql)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsMySql.UpdateCarType(carTypeModelSql));
			}
			foreach (DataRow ms in dt.Rows)
			{
				carTypeModelSql = CarTypeModel.ToObject(ms);
			}

			return carTypeModelSql;
		}

		public int DeleteCarType(string type)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(CarTypeStringsMySql.DeleteCarTypeByType(type));
			}
			return i;
		}

		public int DeleteCarType(int carTypeId)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(CarTypeStringsMySql.DeleteCarTypeById(carTypeId));
			}
			return i;
		}
	}
}
