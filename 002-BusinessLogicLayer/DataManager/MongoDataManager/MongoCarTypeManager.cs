using MongoDB.Driver;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	public class MongoCarTypeManager : ICarTypeRepository
	{
		private readonly IMongoCollection<CarTypeModel> _carTypes;

		public MongoCarTypeManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_carTypes = database.GetCollection<CarTypeModel>(settings.CarTypesCollectionName);
		}


		public MongoCarTypeManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_carTypes = database.GetCollection<CarTypeModel>(ConnectionStrings.CarTypesCollectionName);
		}



		public List<CarTypeModel> GetAllCarTypesIds()
		{
			return _carTypes.Find(ct => true).Project(ct => new CarTypeModel
			{
				carTypeIdMongo = ct.carTypeIdMongo
			}).ToList();
		}

		public List<CarTypeModel> GetAllCarTypes()
		{
			return _carTypes.Find(carType => true).Project(ct => new CarTypeModel
			{
				carTypeIdMongo = ct.carTypeIdMongo,
				carTypeId = ct.carTypeId,
				carType = ct.carType,
				carFirm = ct.carFirm,
				carModel = ct.carModel,
				carDayPrice = ct.carDayPrice,
				carLatePrice = ct.carLatePrice,
				carYear = ct.carYear,
				carGear = ct.carGear
			}).ToList();
		}

		public CarTypeModel GetOneCarType(int typeId)
		{
			return _carTypes.Find<CarTypeModel>(Builders<CarTypeModel>.Filter.Eq(carType => carType.carTypeId, typeId)).Project(ct => new CarTypeModel
			{
				carTypeIdMongo = ct.carTypeIdMongo,
				carTypeId = ct.carTypeId,
				carType = ct.carType,
				carFirm = ct.carFirm,
				carModel = ct.carModel,
				carDayPrice = ct.carDayPrice,
				carLatePrice = ct.carLatePrice,
				carYear = ct.carYear,
				carGear = ct.carGear
			}).FirstOrDefault();
		}

		public CarTypeModel GetOneCarTypeByType(string carType)
		{
			return _carTypes.Find<CarTypeModel>(Builders<CarTypeModel>.Filter.Eq(ct => ct.carType, carType)).Project(ct => new CarTypeModel
			{
				carTypeIdMongo = ct.carTypeIdMongo,
				carTypeId = ct.carTypeId,
				carType = ct.carType,
				carFirm = ct.carFirm,
				carModel = ct.carModel,
				carDayPrice = ct.carDayPrice,
				carLatePrice = ct.carLatePrice,
				carYear = ct.carYear,
				carGear = ct.carGear
			}).FirstOrDefault();
		}


		public CarTypeModel AddCarType(CarTypeModel carTypeModel)
		{
			if (_carTypes.Find<CarTypeModel>(carType => carType.carType.Equals(carTypeModel.carType)).FirstOrDefault() == null)
			{
				_carTypes.InsertOne(carTypeModel);
			}

			CarTypeModel tmpCarTypeModel = GetOneCarTypeByType(carTypeModel.carType);
			return tmpCarTypeModel;
		}

		public CarTypeModel UpdateCarType(CarTypeModel carTypeModel)
		{
			_carTypes.ReplaceOne(carType => carType.carTypeId.Equals(carTypeModel.carTypeId), carTypeModel);
			CarTypeModel tmpCarTypeModel = GetOneCarType(carTypeModel.carTypeId);
			return tmpCarTypeModel;
		}

		public int DeleteCarType(string type)
		{
			_carTypes.DeleteOne(carType => carType.carType.Equals(type));
			return 1;
		}

		public int DeleteCarType(int carTypeId)
		{
			_carTypes.DeleteOne(carType => carType.carTypeId.Equals(carTypeId));
			return 1;
		}
	}
}
