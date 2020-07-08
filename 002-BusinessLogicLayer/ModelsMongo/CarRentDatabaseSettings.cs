namespace RentCarsServerCore
{
	public class CarRentDatabaseSettings : ICarRentDatabaseSettings
	{
		public string BranchesCollectionName { get; set; }
		public string CarForRentCollectionName { get; set; }
		public string CarsCollectionName { get; set; }
		public string CarTypesCollectionName { get; set; }
		public string FullCarDatasCollectionName { get; set; }
		public string MessagesCollectionName { get; set; }
		public string OrderPricesCollectionName { get; set; }
		public string RolesCollectionName { get; set; }
		public string UsersCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}
