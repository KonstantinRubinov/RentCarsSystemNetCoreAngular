using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface ICarsRepository
	{
		List<CarModel> GetAllCars();
		CarModel GetOneCar(string num);
		CarModel AddCar(CarModel carModel);
		CarModel UpdateCar(CarModel carModel);
		string DeleteCar(string num);
		CarModel UploadCarImage(string number, string img);
		List<CarPictureModel> GetAllCarImagesAndNumberOfCars();
		CarPictureModel GetNumberOfCarWithImage(string pictureName);
	}
}
