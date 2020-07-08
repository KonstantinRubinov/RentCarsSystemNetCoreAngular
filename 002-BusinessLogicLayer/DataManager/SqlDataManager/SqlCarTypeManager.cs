using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlCarTypeManager : SqlDataBase, ICarTypeRepository
	{
		public List<CarTypeModel> GetAllCarTypesIds()
		{
			DataTable dt = new DataTable();
			List<CarTypeModel> arrCarTypes = new List<CarTypeModel>();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsSql.GetAllCarTypesIds());
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsSql.GetAllCarTypes());
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsSql.GetOneCarType(typeId));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsSql.AddCarType(carTypeModelSql));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(CarTypeStringsSql.UpdateCarType(carTypeModelSql));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(CarTypeStringsSql.DeleteCarTypeByType(type));
			}
			return i;
		}

		public int DeleteCarType(int carTypeId)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(CarTypeStringsSql.DeleteCarTypeById(carTypeId));
			}
			return i;
		}
	}
}
