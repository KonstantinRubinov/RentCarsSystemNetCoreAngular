using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlSearchManager : MySqlDataBase, ISearchRepository
	{
		public SearchReturnModel GetAllCarsBySearch(SearchModel searchModel, int page = 0, int carsNum = 0)
		{
			DataTable dt = new DataTable();
			List<FullCarDataModel> arrFullCars = new List<FullCarDataModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(SearchStringsMySql.GetAllCarsBySearch(carsNum * page, carsNum, searchModel.freeSearch, searchModel.company, searchModel.carType, searchModel.gear, searchModel.year));
			}
			foreach (DataRow ms in dt.Rows)
			{
				FullCarDataModel fullCarDataModel = FullCarDataModel.ToObject(ms);
				arrFullCars.Add(fullCarDataModel);
			}

			IEnumerable<FullCarDataModel> query = arrFullCars;

			List<FullCarDataModel> fullCars = new List<FullCarDataModel>();

			foreach (FullCarDataModel fullCar in query)
			{
				if (searchModel.fromDate != null && searchModel.toDate != null)
				{
					fullCar.carAvaliable = new MySqlPriceManager().CheckIfCarAvaliable(fullCar.carNumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate);
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


			if (searchReturnModel.fullCarsData != null && searchReturnModel.fullCarsData.Count > 0)
			{
				searchReturnModel.fullCarsDataLenth = searchReturnModel.fullCarsData[0].numerOfCars;
			}
			else
			{
				searchReturnModel.fullCarsDataLenth = 0;
			}

			searchReturnModel.fullCarsDataPage = page;
			return searchReturnModel;
		}
	}
}
