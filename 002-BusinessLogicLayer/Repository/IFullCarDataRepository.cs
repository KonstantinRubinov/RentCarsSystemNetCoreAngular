namespace RentCarsServerCore
{
	public interface IFullCarDataRepository
	{
		FullCarDataModel GetCarAllData(string carNumber);
	}
}
