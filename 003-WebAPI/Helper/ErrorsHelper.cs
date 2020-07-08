using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Diagnostics;

namespace RentCarsServerCore
{
	static class ErrorsHelper
	{
		public static Errors GetErrors(Exception ex)
		{
			Errors errors = new Errors();
#if DEBUG
			errors.Add(GetMostInnerMessage(ex));
			Debug.WriteLine("GetErrors:" + GetMostInnerMessage(ex));
#else
            errors.Add("An error occur, please try again.");
			Debug.WriteLine("An error occur, please try again.");
#endif

			return errors;
		}

		public static Errors GetErrors(ModelStateDictionary modelState)
		{
			Errors errors = new Errors();

			foreach (var prop in modelState.Values) // Values = The illegal value properties.
				foreach (var err in prop.Errors) // err = The single data annotation error.
					errors.Add(err.ErrorMessage);

			return errors;
		}

		public static Errors GetErrors(string message)
		{
			Errors errors = new Errors();

			errors.Add(message);
			Debug.WriteLine("GetErrors: " + message);

			return errors;
		}

		private static string GetMostInnerMessage(Exception ex)
		{
			if (ex.InnerException == null)
				return ex.Message;
			return GetMostInnerMessage(ex.InnerException);
		}
	}
}
