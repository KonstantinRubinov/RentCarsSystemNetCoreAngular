using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface ICarTypeRepository
	{
		List<CarTypeModel> GetAllCarTypesIds();
		List<CarTypeModel> GetAllCarTypes();
		CarTypeModel GetOneCarType(int typeId);
		CarTypeModel AddCarType(CarTypeModel carTypeModelSql);
		CarTypeModel UpdateCarType(CarTypeModel carTypeModelSql);
		int DeleteCarType(string type);
		int DeleteCarType(int carTypeId);
	}
}
