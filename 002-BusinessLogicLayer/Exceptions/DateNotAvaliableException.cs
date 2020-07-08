using System;

namespace RentCarsServerCore
{
	public class DateNotAvaliableException : Exception
	{
		public DateNotAvaliableException()
		{
		}

		public DateNotAvaliableException(string message) : base(message)
		{
		}

		public DateNotAvaliableException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
