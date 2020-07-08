using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlFullCarDataManager : MySqlDataBase, IFullCarDataRepository
	{
		public FullCarDataModel GetCarAllData(string carNumber)
		{
			DataTable dt = new DataTable();
			FullCarDataModel fullCarDataModel = new FullCarDataModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(SearchStringsMySql.GetCarAllDataBySearch(carNumber));
			}
			foreach (DataRow ms in dt.Rows)
			{
				fullCarDataModel = FullCarDataModel.ToObject(ms);
			}

			fullCarDataModel.carAvaliable = new MySqlPriceManager().CheckIfCarAvaliable(carNumber, DateTime.Now, DateTime.Now);

			return fullCarDataModel;
		}
	}
}
