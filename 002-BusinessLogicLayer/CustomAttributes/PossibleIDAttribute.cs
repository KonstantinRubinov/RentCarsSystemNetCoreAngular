using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RentCarsServerCore
{
	public class PossibleIDAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			string IDNum = value.ToString();
			// Validate correct input
			if (!System.Text.RegularExpressions.Regex.IsMatch(IDNum, @"^\d{5,9}$"))
			{
				Debug.WriteLine("The Id " + IDNum + " is impossible");
				return new ValidationResult("The Id " + IDNum + " is impossible");
			}


			// The number is too short - add leading 0000
			if (IDNum.Length < 9)
			{
				while (IDNum.Length < 9)
				{
					IDNum = '0' + IDNum;
				}
				Debug.WriteLine("Add zeros to left of " + IDNum);
			}

			// CHECK THE ID NUMBER
			int mone = 0;
			int incNum;
			for (int i = 0; i < 9; i++)
			{
				incNum = Convert.ToInt32(IDNum[i].ToString());
				incNum *= (i % 2) + 1;
				if (incNum > 9)
					incNum -= 9;
				mone += incNum;
			}
			if (mone % 10 == 0)
			{
				Debug.WriteLine("The Id " + IDNum + " is possible");
				return ValidationResult.Success;
			}

			else
			{
				Debug.WriteLine("The Id " + IDNum + " is impossible");
				return new ValidationResult("The Id " + IDNum + " is impossible");
			}
		}
	}
}
