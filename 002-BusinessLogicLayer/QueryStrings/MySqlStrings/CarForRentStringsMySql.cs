using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class CarForRentStringsMySql
	{
		static private string queryAllCarsForRentString = "SELECT * from CarForRents;";
		static private string queryCarsForRentByCarNumberString = "SELECT * from CarForRents WHERE carNumber = @carNumber;";
		static private string queryCarsForRentByUserIdString = "SELECT CARS.carNumber, CARS.carKm, CARS.carPicture, CARS.carInShape, CARS.carAvaliable, CARS.carBranchID, ALLCARTYPES.thisCarType, ALLCARTYPES.carFirm, ALLCARTYPES.carModel, ALLCARTYPES.carDayPrice, ALLCARTYPES.carLatePrice, ALLCARTYPES.carYear, ALLCARTYPES.carGear, BRANCHES.branchName, BRANCHES.branchAddress, BRANCHES.branchLat, BRANCHES.branchLng From CARS left JOIN ALLCARTYPES ON CARS.carTypeID=ALLCARTYPES.carTypeID left JOIN BRANCHES ON CARS.carBranchID=BRANCHES.branchID WHERE CARFORRENTS.userID = @userID";
		static private string queryCarForRentByRentNumberString = "SELECT * from CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentPost = "INSERT INTO CarForRents (rentStartDate, rentEndDate, rentRealEndDate, userID, carNumber) VALUES (@rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber); SELECT * FROM CarForRents WHERE rentNumber = SCOPE_IDENTITY();";
		static private string queryCarForRentUpdate = "UPDATE CarForRents SET rentStartDate=@rentStartDate, rentEndDate = @rentEndDate, rentRealEndDate = @rentRealEndDate, userID = @userID, carNumber = @carNumber WHERE rentNumber = @rentNumber; SELECT * FROM CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentDeleteRent = "DELETE FROM CarForRents WHERE rentNumber = @rentNumber;";
		static private string queryCarForRentDeleteCar = "DELETE FROM CarForRents WHERE carNumber = @carNumber;";


		static private string procedureAllCarsForRentString = "CALL `rentcar`.`GetAllCarsForRent`();";
		static private string procedureCarsForRentByCarNumberString = "CALL `rentcar`.`GetAllCarsForRentByCarNumber`(@carNumber);";
		static private string procedureCarsForRentByUserIdString = "CALL `rentcar`.`GetAllCarsForRentByUserId`(@userID);";
		static private string procedureCarForRentByRentNumberString = "CALL `rentcar`.`GetOneCarForRentByRentNumber`(@rentNumber);";
		static private string procedureCarForRentPost = "CALL `rentcar`.`AddCarForRent`(@rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber);";
		static private string procedureCarForRentUpdate = "CALL `rentcar`.`UpdateCarForRent`(@rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber, @rentNumber);";
		static private string procedureCarForRentDeleteRent = "CALL `rentcar`.`DeleteCarForRentByRent`(@rentNumber);";
		static private string procedureCarForRentDeleteCar = "CALL `rentcar`.`DeleteCarForRentByCar`(@carNumber);";

		static public MySqlCommand GetAllCarsForRent()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarsForRentString);
			else
				return CreateSqlCommand(procedureAllCarsForRentString);
		}

		static public MySqlCommand GetAllCarsForRentByCarNumber(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandCar(carNumber, queryCarsForRentByCarNumberString);
			else
				return CreateSqlCommandCar(carNumber, procedureCarsForRentByCarNumberString);
		}

		static public MySqlCommand GetAllCarsForRentByUserId(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandUser(userID, queryCarsForRentByUserIdString);
			else
				return CreateSqlCommandUser(userID, procedureCarsForRentByUserIdString);
		}

		static public MySqlCommand GetOneCarForRentByRentNumber(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandRent(rentNumber, queryCarForRentByRentNumberString);
			else
				return CreateSqlCommandRent(rentNumber, procedureCarForRentByRentNumberString);
		}

		static public MySqlCommand AddCarForRent(CarForRentModel carForRentModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carForRentModel, queryCarForRentPost);
			else
				return CreateSqlCommand(carForRentModel, procedureCarForRentPost);
		}

		static public MySqlCommand UpdateCarForRent(CarForRentModel carForRentModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carForRentModel, queryCarForRentUpdate);
			else
				return CreateSqlCommand(carForRentModel, procedureCarForRentUpdate);
		}

		static public MySqlCommand DeleteCarForRentByRent(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandRent(rentNumber, queryCarForRentDeleteRent);
			else
				return CreateSqlCommandRent(rentNumber, procedureCarForRentDeleteRent);
		}

		static public MySqlCommand DeleteCarForRentByCar(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandCar(carNumber, queryCarForRentDeleteCar);
			else
				return CreateSqlCommandCar(carNumber, procedureCarForRentDeleteCar);
		}








		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(CarForRentModel carForRent, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@rentNumber", carForRent.rentNumber);
			command.Parameters.AddWithValue("@userID", carForRent.userID);
			command.Parameters.AddWithValue("@carNumber", carForRent.carNumber);
			command.Parameters.AddWithValue("@rentStartDate", carForRent.rentStartDate);
			command.Parameters.AddWithValue("@rentEndDate", carForRent.rentEndDate);
			command.Parameters.AddWithValue("@rentRealEndDate", carForRent.rentRealEndDate.HasValue ? carForRent.rentRealEndDate : carForRent.rentStartDate.AddDays(-36));

			return command;
		}

		static private MySqlCommand CreateSqlCommandCar(string carNumber, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}

		static private MySqlCommand CreateSqlCommandUser(string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandRent(int rentNumber, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@rentNumber", rentNumber);

			return command;
		}
	}
}
