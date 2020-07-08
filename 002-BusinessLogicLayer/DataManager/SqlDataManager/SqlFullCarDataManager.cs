using System;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlFullCarDataManager : SqlDataBase, IFullCarDataRepository
	{
		public FullCarDataModel GetCarAllData(string carNumber)
		{
			DataTable dt = new DataTable();
			FullCarDataModel fullCarDataModel = new FullCarDataModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(SearchStringsSql.GetCarAllDataBySearch(carNumber));
			}
			foreach (DataRow ms in dt.Rows)
			{
				fullCarDataModel = FullCarDataModel.ToObject(ms);
			}

			fullCarDataModel.carAvaliable = new SqlPriceManager().CheckIfCarAvaliable(carNumber, DateTime.Now, DateTime.Now);

			return fullCarDataModel;
		}
	}
}
