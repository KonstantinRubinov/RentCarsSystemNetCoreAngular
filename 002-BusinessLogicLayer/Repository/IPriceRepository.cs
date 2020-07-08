using System;

namespace RentCarsServerCore
{
	public interface IPriceRepository
	{
		OrderPriceModel priceForOrderIfAvaliable(OrderPriceModel carForPrice);
		FullCarDataModel GetCarDayPrice(string carNumber);
		bool CheckIfCarAvaliable(string carNumber, DateTime fromDate, DateTime toDate);
	}
}
