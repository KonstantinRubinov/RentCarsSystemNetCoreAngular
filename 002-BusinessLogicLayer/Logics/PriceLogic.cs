using System;
using System.Diagnostics;

namespace RentCarsServerCore
{
	public static class PriceLogic
	{
		public static decimal CarPrice(DateTime? start, DateTime? end, decimal dayPrice)
		{
			if (start != null && end != null)
			{
				decimal price = (((DateTime)end - (DateTime)start).Days) * dayPrice;
				Debug.WriteLine("The price is: " + price);
				return price;
			}

			else
			{
				Debug.WriteLine("The price is: " + 0);
				return 0;
			}

		}

		public static decimal TotalPrice(DateTime start, DateTime end, DateTime realEnd, decimal dayPrice, decimal lateDayPrice)
		{
			decimal price = 0;
			decimal latePrice = 0;

			price = CarPrice(start, end, dayPrice);

			if (((realEnd - end).Days) > 0)
			{
				latePrice = CarPrice(end, realEnd, lateDayPrice);
				Debug.WriteLine("The late price is: " + latePrice);
			}

			Debug.WriteLine("The total price is: " + price + latePrice);
			return price + latePrice;
		}
	}
}
