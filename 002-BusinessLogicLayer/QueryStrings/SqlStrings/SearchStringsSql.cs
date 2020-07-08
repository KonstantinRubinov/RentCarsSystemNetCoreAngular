using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class SearchStringsSql
	{
		static private string carData = "Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID";
		static private string carTypeData = "carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear";
		static private string carBranchData = "carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng";

		static private string queryAllCarsString = "SELECT " + carData + ", " + carTypeData + ", " + carBranchData + ", TotalRows = COUNT(*) OVER()" + " " + "from Cars " +
												   "left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID " +
												   "left join Branches carBranches on Cars.carBranchID = carBranches.branchID " +
												   
												   "where ((@searchText IS NOT NULL AND @searchText <> '' AND (Cars.carNumber LIKE '%'+ @searchText + '%' OR carTypes.carFirm LIKE '%' + @searchText + '%' OR carTypes.carModel LIKE '%' + @searchText + '%')) OR (@searchText IS NULL OR @searchText = '')) " +
												   "AND ((@carFirm IS NOT NULL AND @carFirm <> '' AND carTypes.carFirm = @carFirm) OR (@carFirm IS NULL OR @carFirm = '')) " +
												   "AND ((@thisCarType IS NOT NULL AND @thisCarType <> '' AND carTypes.thisCarType = @thisCarType) OR (@thisCarType IS NULL OR @thisCarType = '')) " +
												   "AND ((@carGear IS NOT NULL AND @carGear <> '' AND carTypes.carGear = @carGear) OR (@carGear IS NULL OR @carGear = '')) " +
												   "AND ((NOT @carYear= 0 AND carTypes.carYear = @carYear) OR (@carYear = 0)) "+
												   
												   "ORDER BY Cars.carNumber OFFSET @page ROWS FETCH NEXT @carsNum ROWS ONLY;";
		static private string queryAllCarDataString = "SELECT " + carData + ", " + carTypeData + ", " + carBranchData + " " + "from Cars " +
													  "left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID " +
													  "left join Branches carBranches on cars.carBranchID = carBranches.branchID " +
													  "where Cars.carNumber=@carNumber;";
		static private string queryDayPriceString = "SELECT carTypes.carDayPrice from Cars " +
													"left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID " +
													"where Cars.carNumber=@carNumber;";

		static private string procedureAllCarsString = "EXEC GetAllCarsBySearch @page, @carsNum, @searchText, @carFirm, @thisCarType, @carGear, @carYear;";
		static private string procedureAllCarDataString = "EXEC GetCarAllDataBySearch @carNumber;";
		static private string procedureDayPriceString = "EXEC GetCarDayPriceBySearch @carNumber;";



		static public SqlCommand GetAllCarsBySearch(int page, int carsNum, string searchText="", string carFirm = "", string thisCarType = "", string carGear = "", int carYear=0)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPaging(page, carsNum, searchText, carFirm, thisCarType, carGear, carYear, queryAllCarsString);
			else
				return CreateSqlCommandPaging(page, carsNum, searchText, carFirm, thisCarType, carGear, carYear, procedureAllCarsString);
		}

		static public SqlCommand GetCarAllDataBySearch(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carNumber, queryAllCarDataString);
			else
				return CreateSqlCommand(carNumber, procedureAllCarDataString);
		}

		static public SqlCommand GetCarDayPriceBySearch(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carNumber, queryDayPriceString);
			else
				return CreateSqlCommand(carNumber, procedureDayPriceString);
		}


		

		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommandPaging(int page, int carsNum, string searchText = "", string carFirm = "", string thisCarType = "", string carGear = "", int carYear = 0, string commandText="")
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@page", page);
			command.Parameters.AddWithValue("@carsNum", carsNum);
			command.Parameters.AddWithValue("@searchText", searchText);
			command.Parameters.AddWithValue("@carFirm", carFirm);
			command.Parameters.AddWithValue("@thisCarType", thisCarType);
			command.Parameters.AddWithValue("@carGear", carGear);
			command.Parameters.AddWithValue("@carYear", carYear);

			return command;
		}

		static private SqlCommand CreateSqlCommand(string carNumber, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}
	}
}
