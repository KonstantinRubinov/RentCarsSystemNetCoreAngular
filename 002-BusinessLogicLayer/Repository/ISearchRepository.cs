namespace RentCarsServerCore
{
	public interface ISearchRepository
	{
		SearchReturnModel GetAllCarsBySearch(SearchModel searchModel, int page = 0, int carsNum = 0);
	}
}
