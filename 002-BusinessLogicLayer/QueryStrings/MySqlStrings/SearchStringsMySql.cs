using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class SearchStringsMySql
	{
		static private string carData = "Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID";
		static private string carTypeData = "carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear";
		static private string carBranchData = "carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng";

		static private string queryAllCarsString = "SET @totalRows = ( " +
												   "SELECT COUNT(*) " +
												   "from Cars " +
												   "left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID " +
												   "left join Branches carBranches on Cars.carBranchID = carBranches.branchID " +
												   "where ((@searchText IS NOT NULL AND @searchText <> '' AND (Cars.carNumber LIKE CONCAT('%', @searchText, '%') OR carTypes.carFirm LIKE CONCAT('%', @searchText, '%') OR carTypes.carModel LIKE CONCAT('%', @searchText, '%'))) OR (@searchText IS NULL OR @searchText = '')) " +
												   "AND ((@carFirm IS NOT NULL AND @carFirm <> '' AND carTypes.carFirm = @carFirm) OR (@carFirm IS NULL OR @carFirm = '')) " +
												   "AND ((@thisCarType IS NOT NULL AND @thisCarType <> '' AND carTypes.thisCarType = @thisCarType) OR (@thisCarType IS NULL OR @thisCarType = '')) " +
												   "AND ((@carGear IS NOT NULL AND @carGear <> '' AND carTypes.carGear = @carGear) OR (@carGear IS NULL OR @carGear = '')) " +
												   "AND ((NOT @carYear= 0 AND carTypes.carYear = @carYear) OR (@carYear = 0)) " +
												   "); " +

												   "SELECT " + carData + ", " + carTypeData + ", " + carBranchData + ", @totalRows" + " " + "from Cars " +
												   "left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID " +
												   "left join Branches carBranches on Cars.carBranchID = carBranches.branchID " +
												   
												   "where ((@searchText IS NOT NULL AND @searchText <> '' AND (Cars.carNumber LIKE CONCAT('%', @searchText, '%') OR carTypes.carFirm LIKE CONCAT('%', @searchText, '%') OR carTypes.carModel LIKE CONCAT('%', @searchText, '%'))) OR (@searchText IS NULL OR @searchText = '')) " +
												   "AND ((@carFirm IS NOT NULL AND @carFirm <> '' AND carTypes.carFirm = @carFirm) OR (@carFirm IS NULL OR @carFirm = '')) " +
												   "AND ((@thisCarType IS NOT NULL AND @thisCarType <> '' AND carTypes.thisCarType = @thisCarType) OR (@thisCarType IS NULL OR @thisCarType = '')) " +
												   "AND ((@carGear IS NOT NULL AND @carGear <> '' AND carTypes.carGear = @carGear) OR (@carGear IS NULL OR @carGear = '')) " +
												   "AND ((NOT @carYear= 0 AND carTypes.carYear = @carYear) OR (@carYear = 0)) " +
												   
												   "ORDER BY Cars.carNumber LIMIT @carsNum OFFSET @page;";
		static private string queryAllCarDataString = "SELECT " + carData + ", " + carTypeData + ", " + carBranchData + " " + "from Cars " +
													"left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID " +
													"left join Branches carBranches on cars.carBranchID = carBranches.branchID " +
													"where Cars.carNumber=@carNumber;";
		static private string queryDayPriceString = "SELECT carTypes.carDayPrice from Cars " +
													"left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID " +
													"where Cars.carNumber=@carNumber;";


		static private string procedureAllCarsString = "CALL `rentcar`.`GetAllCarsBySearch`(@page, @carsNum, @searchText, @carFirm, @thisCarType, @carGear, @carYear);";
		static private string procedureAllCarDataString = "CALL `rentcar`.`GetCarAllDataBySearch`(@carNumber);";
		static private string procedureDayPriceString = "CALL `rentcar`.`GetCarDayPriceBySearch`(@carNumber);";


		static public MySqlCommand GetAllCarsBySearch(int page, int carsNum, string searchText = "", string carFirm = "", string thisCarType = "", string carGear = "", int carYear = 0)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPaging(page, carsNum, searchText, carFirm, thisCarType, carGear, carYear, queryAllCarsString);
			else
				return CreateSqlCommandPaging(page, carsNum, searchText, carFirm, thisCarType, carGear, carYear, procedureAllCarsString);
		}

		static public MySqlCommand GetCarAllDataBySearch(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carNumber, queryAllCarDataString);
			else
				return CreateSqlCommand(carNumber, procedureAllCarDataString);
		}

		static public MySqlCommand GetCarDayPriceBySearch(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carNumber, queryDayPriceString);
			else
				return CreateSqlCommand(carNumber, procedureDayPriceString);
		}




		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommandPaging(int page, int carsNum, string searchText = "", string carFirm = "", string thisCarType = "", string carGear = "", int carYear = 0, string commandText = "")
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@page", page);
			command.Parameters.AddWithValue("@carsNum", carsNum);
			command.Parameters.AddWithValue("@searchText", searchText);
			command.Parameters.AddWithValue("@carFirm", carFirm);
			command.Parameters.AddWithValue("@thisCarType", thisCarType);
			command.Parameters.AddWithValue("@carGear", carGear);
			command.Parameters.AddWithValue("@carYear", carYear);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(string carNumber, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}
	}
}
