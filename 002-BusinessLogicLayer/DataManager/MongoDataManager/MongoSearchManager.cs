using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentCarsServerCore
{
	public class MongoSearchManager : ISearchRepository
	{
		private readonly IMongoCollection<CarModel> _cars;
		private readonly IMongoCollection<CarTypeModel> _carTypes;
		private readonly IMongoCollection<BranchModel> _branches;

		public MongoSearchManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_cars = database.GetCollection<CarModel>(settings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(settings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(settings.BranchesCollectionName);
		}

		public MongoSearchManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_cars = database.GetCollection<CarModel>(ConnectionStrings.CarsCollectionName);
			_carTypes = database.GetCollection<CarTypeModel>(ConnectionStrings.CarTypesCollectionName);
			_branches = database.GetCollection<BranchModel>(ConnectionStrings.BranchesCollectionName);
		}

		public SearchReturnModel GetAllCarsBySearch(SearchModel searchModel, int page = 0, int carsNum = 0)
		{
			var resultQuary =
			from cars in _cars.AsQueryable()
			join carTypes in _carTypes.AsQueryable() on cars.carTypeIDMongo equals carTypes.carTypeIdMongo
			join carBranches in _branches.AsQueryable() on cars.carBranchIDMongo equals carBranches.branchIDMongo
			where ((searchModel.freeSearch != null && !searchModel.freeSearch.Equals("") && (cars.carNumber.Contains(searchModel.freeSearch) || carTypes.carFirm.Contains(searchModel.freeSearch) || carTypes.carModel.Contains(searchModel.freeSearch))) || (searchModel.freeSearch == null || searchModel.freeSearch.Equals("")))
			where ((searchModel.company != null && !searchModel.company.Equals("") && carTypes.carFirm.Equals(searchModel.company)) || (searchModel.company == null || searchModel.company.Equals("")))
			where ((searchModel.carType != null && !searchModel.carType.Equals("") && carTypes.carType.Equals(searchModel.carType)) || (searchModel.carType == null || searchModel.carType.Equals("")))
			where ((searchModel.gear != null && !searchModel.gear.Equals("") && carTypes.carGear.Equals(searchModel.gear)) || (searchModel.gear == null || searchModel.gear.Equals("")))
			where ((searchModel.year != 0 && carTypes.carYear.Equals(searchModel.year)) || (searchModel.year == 0))

			select new FullCarDataModel
			{
				carNumber = cars.carNumber,
				carKm = cars.carKm,
				carPicture = cars.carPicture != null ? "/assets/images/cars/" + cars.carPicture : null,
				carInShape = cars.carInShape,
				carAvaliable = cars.carAvaliable,
				carBranchIDMongo = cars.carBranchIDMongo,

				carType = carTypes.carType,
				carFirm = carTypes.carFirm,
				carModel = carTypes.carModel,
				carDayPrice = carTypes.carDayPrice,
				carLatePrice = carTypes.carLatePrice,
				carYear = carTypes.carYear,
				carGear = carTypes.carGear,

				branchName = carBranches.branchName,
				branchAddress = carBranches.branchAddress,
				branchLat = carBranches.branchLat,
				branchLng = carBranches.branchLng
			};

			var total = resultQuary.Count();
			resultQuary = resultQuary.OrderBy(c => c.carNumber).Skip(page * carsNum).Take(carsNum);


			List<FullCarDataModel> fullCars = new List<FullCarDataModel>();

			foreach (FullCarDataModel fullCar in resultQuary)
			{
				if (searchModel.fromDate != null && searchModel.toDate != null)
				{
					fullCar.carAvaliable = new MongoPriceManager().CheckIfCarAvaliable(fullCar.carNumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate);
					if (fullCar.carAvaliable)
					{
						fullCar.carPrice = PriceLogic.CarPrice(searchModel.fromDate, searchModel.toDate, fullCar.carDayPrice);
						fullCars.Add(fullCar);
					}
				}
				else
				{
					fullCars.Add(fullCar);
				}
			}

			SearchReturnModel searchReturnModel = new SearchReturnModel();

			searchReturnModel.fullCarsData = fullCars;
			searchReturnModel.fullCarsDataLenth = total;
			searchReturnModel.fullCarsDataPage = page;
			return searchReturnModel;
		}
	}
}
