namespace RentCarsServerCore
{
	public interface ICarRentDatabaseSettings
	{
		string BranchesCollectionName { get; set; }
		string CarForRentCollectionName { get; set; }
		string CarsCollectionName { get; set; }
		string CarTypesCollectionName { get; set; }
		string FullCarDatasCollectionName { get; set; }
		string MessagesCollectionName { get; set; }
		string OrderPricesCollectionName { get; set; }
		string RolesCollectionName { get; set; }
		string UsersCollectionName { get; set; }

		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
