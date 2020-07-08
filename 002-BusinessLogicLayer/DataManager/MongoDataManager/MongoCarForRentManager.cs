using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentCarsServerCore
{
	public class MongoCarForRentManager : ICarForRentRepository
	{
		private readonly IMongoCollection<CarForRentModel> _carsForRent;
		private readonly IMongoCollection<CarModel> _cars;
		private readonly IMongoCollection<CarTypeModel> _carTypes;
		private readonly IMongoCollection<BranchModel> _branches;

		public MongoCarForRentManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_cars = database.GetCollection<CarModel>(settings.CarsCollectionName);
			_carsForRent = database.GetCollection<CarForRentModel>(settings.CarForRentCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(settings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(settings.BranchesCollectionName);
		}

		public MongoCarForRentManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_cars = database.GetCollection<CarModel>(ConnectionStrings.CarsCollectionName);
			_carsForRent = database.GetCollection<CarForRentModel>(ConnectionStrings.CarForRentCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(ConnectionStrings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(ConnectionStrings.BranchesCollectionName);
		}

		public List<CarForRentModel> GetAllCarsForRent()
		{
			return _carsForRent.Find(carsForRent => true).Project(cfr => new CarForRentModel
			{
				rentNumber = cfr.rentNumber,
				userID = cfr.userID,
				carNumber = cfr.carNumber,
				rentStartDate = cfr.rentStartDate,
				rentEndDate = cfr.rentEndDate,
				rentRealEndDate = cfr.rentRealEndDate
			}).ToList();
		}

		public List<CarForRentModel> GetCarsForRentByCarNumber(string carNumber)
		{
			if (carNumber.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _carsForRent.Find<CarForRentModel>(Builders<CarForRentModel>.Filter.Eq(carsForRent => carsForRent.carNumber, carNumber)).Sort("{rentStartDate: 1}").Project(cfr => new CarForRentModel
			{
				rentNumber = cfr.rentNumber,
				userID = cfr.userID,
				carNumber = cfr.carNumber,
				rentStartDate = cfr.rentStartDate,
				rentEndDate = cfr.rentEndDate,
				rentRealEndDate = cfr.rentRealEndDate
			}).ToList();
		}


		public List<FullCarDataModel> GetCarsForRentByUserId(string userID)
		{
			if (userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return (from carsForRent in _carsForRent.AsQueryable()
					join car in _cars.AsQueryable() on carsForRent.carNumber equals car.carNumber
					join carType in _carTypes.AsQueryable() on car.carTypeIDMongo equals carType.carTypeIdMongo
					join branch in _branches.AsQueryable() on car.carBranchIDMongo equals branch.branchIDMongo

					where (carsForRent.userID.Equals(userID))
					select new FullCarDataModel
					{
						carNumber = car.carNumber,
						carKm = car.carKm,
						carPicture = car.carPicture != null ? "/assets/images/cars/" + car.carPicture : null,
						carInShape = car.carInShape,
						carAvaliable = car.carAvaliable,
						carBranchID = car.carBranchID,

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
					}).ToList();
		}

		public CarForRentModel GetOneCarForRentByRentNumber(int rentNumber)
		{
			if (rentNumber < 0)
				throw new ArgumentOutOfRangeException();

			return _carsForRent.Find<CarForRentModel>(Builders<CarForRentModel>.Filter.Eq(carsForRent => carsForRent.rentNumber, rentNumber)).Project(cfr => new CarForRentModel
			{
				rentNumber = cfr.rentNumber,
				userID = cfr.userID,
				carNumber = cfr.carNumber,
				rentStartDate = cfr.rentStartDate,
				rentEndDate = cfr.rentEndDate,
				rentRealEndDate = cfr.rentRealEndDate
			}).FirstOrDefault();
		}

		public CarForRentModel AddCarForRent(CarForRentModel carForRentModel)
		{
			if (carForRentModel.rentRealEndDate == null)
				carForRentModel.rentRealEndDate = carForRentModel.rentStartDate.AddDays(-36);

			_carsForRent.InsertOne(carForRentModel);
			CarForRentModel tmpCarForRentModel = GetOneCarForRentByRentNumber(carForRentModel.rentNumber);
			return tmpCarForRentModel;
		}

		public CarForRentModel UpdateCarForRent(CarForRentModel carForRentModel)
		{
			if (carForRentModel.rentRealEndDate == null)
				carForRentModel.rentRealEndDate = carForRentModel.rentStartDate.AddDays(-36);

			_carsForRent.ReplaceOne(carsForRent => carsForRent.rentNumber == carForRentModel.rentNumber, carForRentModel);
			CarForRentModel tmpCarForRentModel = GetOneCarForRentByRentNumber(carForRentModel.rentNumber);
			return tmpCarForRentModel;
		}

		public int DeleteCarForRent(int rentNumber)
		{
			_carsForRent.DeleteOne(carsForRent => carsForRent.rentNumber == rentNumber);
			return 1;
		}

		public int DeleteCarForRent(string carNumber)
		{
			_carsForRent.DeleteMany(carsForRent => carsForRent.carNumber == carNumber);
			return 1;
		}
	}
}
