using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlPriceManager : SqlDataBase, IPriceRepository
	{
		public FullCarDataModel GetCarDayPrice(string carNumber)
		{
			DataTable dt = new DataTable();
			FullCarDataModel fullCarDataModel = new FullCarDataModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(SearchStringsSql.GetCarDayPriceBySearch(carNumber));
			}
			foreach (DataRow ms in dt.Rows)
			{
				fullCarDataModel.carDayPrice = decimal.Parse(ms[0].ToString());
			}

			return fullCarDataModel;
		}

		public OrderPriceModel priceForOrderIfAvaliable(OrderPriceModel carForPrice)
		{
			MySqlFullCarDataManager sqlFullCarDataLogic = new MySqlFullCarDataManager();
			bool isAvaliable = CheckIfCarAvaliable(carForPrice.carNumber, carForPrice.rentStartDate, carForPrice.rentEndDate) == true;
			if (isAvaliable == true)
			{
				FullCarDataModel myCarsForRentModel = GetCarDayPrice(carForPrice.carNumber);

				carForPrice.orderDays = ((carForPrice.rentEndDate - carForPrice.rentStartDate).Days);
				carForPrice.carPrice = PriceLogic.CarPrice(carForPrice.rentStartDate, carForPrice.rentEndDate, myCarsForRentModel.carDayPrice);
			}
			else
			{
				throw new DateNotAvaliableException("The Car Is Not Avaliable at this dates");
			}
			return carForPrice;
		}

		public bool CheckIfCarAvaliable(string carNumber, DateTime fromDate, DateTime toDate)
		{
			List<CarForRentModel> carForRentList = new SqlCarForRentManager().GetCarsForRentByCarNumber(carNumber);

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
