namespace RentCarsServerCore
{
	static public class ConnectionStrings
	{
		static private string sqlConnectionString = "Data Source =.; Initial Catalog = RentCar; Integrated Security = True";
		static private string mySqlConnectionString = "server=localhost; user id = root; persistsecurityinfo=True; password=Rk14101981; database=RentCar; UseAffectedRows=True; Allow User Variables=True";

		static public string ConnectionString = "mongodb://localhost:27017";
		static public string DatabaseName = "RentCars";
		static public string BranchesCollectionName = "Branches";
		static public string CarForRentCollectionName = "CarForRents";
		static public string CarsCollectionName = "Cars";
		static public string CarTypesCollectionName = "CarTypes";
		static public string FullCarDatasCollectionName = "FullCarDatas";
		static public string MessagesCollectionName = "Messages";
		static public string OrderPricesCollectionName = "OrderPrices";
		static public string RolesCollectionName = "Roles";
		static public string UsersCollectionName = "Users";

		static public string GetSqlConnection()
		{
			return sqlConnectionString;
		}

		static public string GetMySqlConnection()
		{
			return mySqlConnectionString;
		}
	}
}
