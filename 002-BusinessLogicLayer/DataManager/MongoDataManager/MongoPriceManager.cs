using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RentCarsServerCore
{
	public class MongoPriceManager : IPriceRepository
	{
		private readonly IMongoCollection<CarForRentModel> _carsForRent;
		private readonly IMongoCollection<CarModel> _cars;
		private readonly IMongoCollection<CarTypeModel> _carTypes;
		private readonly IMongoCollection<BranchModel> _branches;

		public MongoPriceManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_carsForRent = database.GetCollection<CarForRentModel>(settings.CarForRentCollectionName);
			_cars = database.GetCollection<CarModel>(settings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(settings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(settings.BranchesCollectionName);
		}

		public MongoPriceManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_carsForRent = database.GetCollection<CarForRentModel>(ConnectionStrings.CarForRentCollectionName);
			_cars = database.GetCollection<CarModel>(ConnectionStrings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(ConnectionStrings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(ConnectionStrings.BranchesCollectionName);
		}

		public OrderPriceModel priceForOrderIfAvaliable(OrderPriceModel carForPrice)
		{
			bool isAvaliable = CheckIfCarAvaliable(carForPrice.carNumber, carForPrice.rentStartDate, carForPrice.rentEndDate) == true;
			if (isAvaliable == true)
			{
				FullCarDataModel myCarsForRentModel = GetCarDayPrice(carForPrice.carNumber);

				carForPrice.orderDays = ((carForPrice.rentEndDate - carForPrice.rentStartDate).Days);
				carForPrice.carPrice = PriceLogic.CarPrice(carForPrice.rentStartDate, carForPrice.rentEndDate, myCarsForRentModel.carDayPrice);
			}
			else
			{
				Debug.WriteLine("priceForOrderIfAvaliable DateNotAvaliableException: " + "The Car Is Not Avaliable at this dates");
				throw new DateNotAvaliableException("The Car Is Not Avaliable at this dates");
			}
			return carForPrice;
		}

		public FullCarDataModel GetCarDayPrice(string carNumber)
		{
			return (from carsForRent in _carsForRent.AsQueryable()
					join car in _cars.AsQueryable() on carsForRent.carNumber equals car.carNumber
					join carType in _carTypes.AsQueryable() on car.carTypeID equals carType.carTypeId
					join branch in _branches.AsQueryable() on car.carBranchID equals branch.branchID
					where (car.carNumber.Equals(carNumber))
					select new FullCarDataModel
					{
						carDayPrice = carType.carDayPrice
					}).FirstOrDefault();
		}

		public bool CheckIfCarAvaliable(string carNumber, DateTime fromDate, DateTime toDate)
		{
			List<CarForRentModel> carForRentList = new MongoCarForRentManager().GetCarsForRentByCarNumber(carNumber);
			if (carForRentList != null && carForRentList.Count > 0)
			{
				if (DateTime.Compare((DateTime)toDate, carForRentList[0].rentStartDate) < 0)
				{
					return true;
				}
				if (DateTime.Compare((DateTime)fromDate, carForRentList[carForRentList.Count - 1].rentEndDate) > 0)
				{
					return true;
				}

				for (int i = 0; i < carForRentList.Count - 1; i++)
				{
					if (DateTime.Compare((DateTime)fromDate, carForRentList[i].rentEndDate) > 0 && DateTime.Compare((DateTime)toDate, carForRentList[i + 1].rentStartDate) < 0)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
