using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace RentCarsServerCore
{
	public class MongoCarsManager : ICarsRepository
	{
		private readonly IMongoCollection<CarModel> _cars;

		public MongoCarsManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_cars = database.GetCollection<CarModel>(settings.CarsCollectionName);
		}

		public MongoCarsManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_cars = database.GetCollection<CarModel>(ConnectionStrings.CarsCollectionName);
		}

		public List<CarModel> GetAllCars()
		{
			return _cars.Find(car => true).Project(cars => new CarModel
			{
				carNumber = cars.carNumber,
				carTypeIDMongo = cars.carTypeIDMongo,
				carKm = cars.carKm,
				carPicture = cars.carPicture != null ? "/assets/images/cars/" + cars.carPicture : null,
				carInShape = cars.carInShape,
				carAvaliable = cars.carAvaliable,
				carBranchIDMongo = cars.carBranchIDMongo
			}).ToList();
		}

		public CarModel GetOneCar(string num)
		{
			return _cars.Find(Builders<CarModel>.Filter.Eq(cars => cars.carNumber, num)).Project(cars => new CarModel
			{
				carNumber = cars.carNumber,
				carTypeIDMongo = cars.carTypeIDMongo,
				carKm = cars.carKm,
				carPicture = cars.carPicture != null ? "/assets/images/cars/" + cars.carPicture : null,
				carInShape = cars.carInShape,
				carAvaliable = cars.carAvaliable,
				carBranchIDMongo = cars.carBranchIDMongo
			}).SingleOrDefault();
		}

		public List<CarPictureModel> GetAllCarImagesAndNumberOfCars()
		{
			return (from car in _cars.AsQueryable()
					group car by car.carPicture into pictureGroup
					select new CarPictureModel()
					{
						carPictureLink = "/assets/images/cars/" + pictureGroup.Key,
						carPictureName = pictureGroup.Key,
						numberOfCars = pictureGroup.Select(c => c.carNumber).Count()
					}).ToList();
		}


		public CarPictureModel GetNumberOfCarWithImage(string pictureName)
		{
			return (from car in _cars.AsQueryable()
					where car.carPicture.Equals(pictureName)
					group car by car.carPicture into pictureGroup
					select new CarPictureModel()
					{
						carPictureLink = "/assets/images/cars/" + pictureGroup.Key,
						carPictureName = pictureGroup.Key,
						numberOfCars = pictureGroup.Select(c => c.carNumber).Count()
					}).FirstOrDefault();
		}

		public CarModel AddCar(CarModel carModel)
		{
			if (GetOneCar(carModel.carNumber) == null)
			{
				_cars.InsertOne(carModel);
			}

			CarModel tmpCarModel = GetOneCar(carModel.carNumber);
			return tmpCarModel;
		}

		public CarModel UpdateCar(CarModel carModel)
		{
			_cars.ReplaceOne(car => car.carNumber.Equals(carModel.carNumber), carModel);
			CarModel tmpCarModel = GetOneCar(carModel.carNumber);
			return tmpCarModel;
		}

		public string DeleteCar(string num)
		{
			CarModel car = GetOneCar(num);
			string str = car.carPicture;
			_cars.DeleteOne(c => c.carNumber.Equals(num));
			return str;
		}

		public CarModel UploadCarImage(string carNumber, string img)
		{
			CarModel carModel = GetOneCar(carNumber);
			carModel.carPicture = img;

			_cars.ReplaceOne(car => car.carNumber.Equals(carModel.carNumber), carModel);
			CarModel tmpCarModel = GetOneCar(carModel.carNumber);
			return tmpCarModel;
		}
	}
}
