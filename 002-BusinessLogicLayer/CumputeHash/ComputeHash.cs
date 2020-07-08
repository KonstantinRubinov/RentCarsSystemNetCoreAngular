using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace RentCarsServerCore
{
	public static class ComputeHash
	{
		public static string ComputeNewHash(string hashedPassword)
		{
			var dataAsBytes = Encoding.UTF8.GetBytes(hashedPassword);
			using (var hasher = new HMACSHA256(dataAsBytes))
			{
				Debug.WriteLine("ComputeNewHash from:" + hashedPassword);
				return Convert.ToBase64String(hasher.ComputeHash(dataAsBytes));
			}
		}
	}
}
