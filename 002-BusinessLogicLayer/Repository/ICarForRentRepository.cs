using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface ICarForRentRepository
	{
		List<CarForRentModel> GetAllCarsForRent();
		List<CarForRentModel> GetCarsForRentByCarNumber(string carNumber);
		List<FullCarDataModel> GetCarsForRentByUserId(string userID);
		CarForRentModel GetOneCarForRentByRentNumber(int rentNumber);
		CarForRentModel AddCarForRent(CarForRentModel carForRentModel);
		CarForRentModel UpdateCarForRent(CarForRentModel carForRentModel);
		int DeleteCarForRent(int rentNumber);
		int DeleteCarForRent(string carNumber);
	}
}
