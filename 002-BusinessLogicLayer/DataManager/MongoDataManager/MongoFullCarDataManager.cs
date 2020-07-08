using MongoDB.Driver;
using System;
using System.Linq;

namespace RentCarsServerCore
{
	public class MongoFullCarDataManager : IFullCarDataRepository
	{
		private readonly IMongoCollection<CarModel> _cars;
		private readonly IMongoCollection<CarTypeModel> _carTypes;
		private readonly IMongoCollection<BranchModel> _branches;

		public MongoFullCarDataManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_cars = database.GetCollection<CarModel>(settings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(settings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(settings.BranchesCollectionName);
		}

		public MongoFullCarDataManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_cars = database.GetCollection<CarModel>(ConnectionStrings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(ConnectionStrings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(ConnectionStrings.BranchesCollectionName);
		}

		public FullCarDataModel GetCarAllData(string carNumber)
		{
			var resultQuary = (from car in _cars.AsQueryable()
							   join carType in _carTypes.AsQueryable() on car.carTypeIDMongo equals carType.carTypeIdMongo
							   join branch in _branches.AsQueryable() on car.carBranchIDMongo equals branch.branchIDMongo
							   where (car.carNumber.Equals(carNumber))
							   select new FullCarDataModel
							   {
								   carNumber = car.carNumber,
								   carKm = car.carKm,
								   carPicture = car.carPicture != null ? "/assets/images/cars/" + car.carPicture : null,
								   carInShape = car.carInShape,
								   carAvaliable = car.carAvaliable,
								   carBranchIDMongo = car.carBranchIDMongo,

								   carType = carType.carType,
								   carFirm = carType.carFirm,
								   carModel = carType.carModel,
								   carDayPrice = carType.carDayPrice,
								   carLatePrice = carType.carLatePrice,
								   carYear = carType.carYear,
								   carGear = carType.carGear,

								   branchName = branch.branchName,
								   branchAddress = branch.branchAddress,
								   branchLat = branch.branchLat,
								   branchLng = branch.branchLng
							   });

			resultQuary.SingleOrDefault().carAvaliable = new MongoPriceManager().CheckIfCarAvaliable(carNumber, DateTime.Now, DateTime.Now);
			return resultQuary.SingleOrDefault();
		}
	}
}
