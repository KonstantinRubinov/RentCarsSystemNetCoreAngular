using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class CarForRentStringsSql
	{
		static private string queryAllCarsForRentString = "SELECT * from CarForRents;";
		static private string queryCarsForRentByCarNumberString = "SELECT * from CarForRents WHERE carNumber = @carNumber;";
		static private string queryCarsForRentByUserIdString = "SELECT CARS.carNumber, CARS.carKm, CARS.carPicture, CARS.carInShape, CARS.carAvaliable, CARS.carBranchID, ALLCARTYPES.thisCarType, ALLCARTYPES.carFirm, ALLCARTYPES.carModel, ALLCARTYPES.carDayPrice, ALLCARTYPES.carLatePrice, ALLCARTYPES.carYear, ALLCARTYPES.carGear, BRANCHES.branchName, BRANCHES.branchAddress, BRANCHES.branchLat, BRANCHES.branchLng From CARS left JOIN ALLCARTYPES ON CARS.carTypeID=ALLCARTYPES.carTypeID left JOIN BRANCHES ON CARS.carBranchID=BRANCHES.branchID WHERE CARFORRENTS.userID = @userID;";
		static private string queryCarForRentByRentNumberString = "SELECT * from CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentPost = "INSERT INTO CarForRents (rentStartDate, rentEndDate, rentRealEndDate, userID, carNumber) VALUES (@rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber); SELECT * FROM CarForRents WHERE rentNumber = SCOPE_IDENTITY();";
		static private string queryCarForRentUpdate = "UPDATE CarForRents SET rentStartDate=@rentStartDate, rentEndDate = @rentEndDate, rentRealEndDate = @rentRealEndDate, userID = @userID, carNumber = @carNumber WHERE rentNumber = @rentNumber; SELECT * FROM CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentDeleteRent = "DELETE FROM CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentDeleteCar = "DELETE FROM CarForRents WHERE carNumber = @carNumber;";

		static private string procedureAllCarsForRentString = "EXEC GetAllCarsForRent;";
		static private string procedureCarsForRentByCarNumberString = "EXEC GetAllCarsForRentByCarNumber @carNumber;";
		static private string procedureCarsForRentByUserIdString = "EXEC GetAllCarsForRentByUserId @userID;";
		static private string procedureCarForRentByRentNumberString = "EXEC GetOneCarForRentByRentNumber @rentNumber;";
		static private string procedureCarForRentPost = "EXEC AddCarForRent @rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber;";
		static private string procedureCarForRentUpdate = "EXEC UpdateCarForRent @rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber, @rentNumber;";
		static private string procedureCarForRentDeleteRent = "EXEC DeleteCarForRentByRent @rentNumber;";
		static private string procedureCarForRentDeleteCar = "EXEC DeleteCarForRentByCar @carNumber;";

		static public SqlCommand GetAllCarsForRent()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarsForRentString);
			else
				return CreateSqlCommand(procedureAllCarsForRentString);
		}

		static public SqlCommand GetAllCarsForRentByCarNumber(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandCar(carNumber, queryCarsForRentByCarNumberString);
			else
				return CreateSqlCommandCar(carNumber, procedureCarsForRentByCarNumberString);
		}

		static public SqlCommand GetAllCarsForRentByUserId(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandUser(userID, queryCarsForRentByUserIdString);
			else
				return CreateSqlCommandUser(userID, procedureCarsForRentByUserIdString);
		}

		static public SqlCommand GetOneCarForRentByRentNumber(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandRent(rentNumber, queryCarForRentByRentNumberString);
			else
				return CreateSqlCommandRent(rentNumber, procedureCarForRentByRentNumberString);
		}

		static public SqlCommand AddCarForRent(CarForRentModel carForRentModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carForRentModel, queryCarForRentPost);
			else
				return CreateSqlCommand(carForRentModel, procedureCarForRentPost);
		}

		static public SqlCommand UpdateCarForRent(CarForRentModel carForRentModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carForRentModel, queryCarForRentUpdate);
			else
				return CreateSqlCommand(carForRentModel, procedureCarForRentUpdate);
		}

		static public SqlCommand DeleteCarForRentByRent(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandRent(rentNumber, queryCarForRentDeleteRent);
			else
				return CreateSqlCommandRent(rentNumber, procedureCarForRentDeleteRent);
		}

		static public SqlCommand DeleteCarForRentByCar(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandCar(carNumber, queryCarForRentDeleteCar);
			else
				return CreateSqlCommandCar(carNumber, procedureCarForRentDeleteCar);
		}



		




		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommand(CarForRentModel carForRent, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@rentNumber", carForRent.rentNumber);
			command.Parameters.AddWithValue("@userID", carForRent.userID);
			command.Parameters.AddWithValue("@carNumber", carForRent.carNumber);
			command.Parameters.AddWithValue("@rentStartDate", carForRent.rentStartDate);
			command.Parameters.AddWithValue("@rentEndDate", carForRent.rentEndDate);
			command.Parameters.AddWithValue("@rentRealEndDate", carForRent.rentRealEndDate.HasValue ? carForRent.rentRealEndDate: carForRent.rentStartDate.AddDays(-36));

			return command;
		}

		static private SqlCommand CreateSqlCommandCar(string carNumber, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}

		static private SqlCommand CreateSqlCommandUser(string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private SqlCommand CreateSqlCommandRent(int rentNumber, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@rentNumber", rentNumber);

			return command;
		}
	}
}
