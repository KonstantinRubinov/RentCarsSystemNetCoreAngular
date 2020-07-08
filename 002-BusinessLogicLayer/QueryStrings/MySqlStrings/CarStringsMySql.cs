using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class CarStringsMySql
	{
		static private string queryAllCarsString = "SELECT * from Cars;";
		static private string queryCarByNumberString = "SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarPost = "INSERT INTO Cars (carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID) VALUES (@carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID); SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarUpdate = "UPDATE Cars SET carKm = @carKm, carPicture=@carPicture, carInShape = @carInShape, carAvaliable = @carAvaliable, carNumber = @carNumber, carBranchID = @carBranchID, carTypeID = @carTypeID WHERE carNumber = @carNumber; SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarDelete = "SELECT carPicture from Cars WHERE carNumber = @carNumber; DELETE FROM Cars WHERE carNumber = @carNumber;";
		static private string queryAllCarImagesAndNumberOfCars = "SELECT carPicture, COUNT(carNumber) FROM Cars GROUP BY carPicture;";
		static private string queryNumberOfCarWithImage = "SELECT carPicture, COUNT(carNumber) FROM Cars WHERE carPicture = @carPicture GROUP BY carPicture;";

		static private string procedureAllCarsString = "CALL `rentcar`.`GetAllCars`();";
		static private string procedureCarByNumberString = "CALL `rentcar`.`GetOneCarByNumber`(@carNumber);";
		static private string procedureCarPost = "CALL `rentcar`.`AddCar`(@carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID);";
		static private string procedureCarUpdate = "CALL `rentcar`.`UpdateCar`(@carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID);";
		static private string procedureCarDelete = "CALL `rentcar`.`DeleteCar`(@carNumber);";
		static private string procedureAllCarImagesAndNumberOfCars = "CALL `rentcar`.`GetAllCarImagesAndNumberOfCars`();";
		static private string procedureNumberOfCarWithImage = "CALL `rentcar`.`GetNumberOfCarWithImage`(@carPicture);";


		static public MySqlCommand GetAllCars()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarsString);
			else
				return CreateSqlCommand(procedureAllCarsString);
		}

		static public MySqlCommand GetOneCarByNumber(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandNumber(carNumber, queryCarByNumberString);
			else
				return CreateSqlCommandNumber(carNumber, procedureCarByNumberString);
		}

		static public MySqlCommand AddCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carModel, queryCarPost);
			else
				return CreateSqlCommand(carModel, procedureCarPost);
		}

		static public MySqlCommand UpdateCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carModel, queryCarUpdate);
			else
				return CreateSqlCommand(carModel, procedureCarUpdate);
		}

		static public MySqlCommand DeleteCar(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandNumber(carNumber, queryCarDelete);
			else
				return CreateSqlCommandNumber(carNumber, procedureCarDelete);
		}

		static public MySqlCommand GetAllCarImagesAndNumberOfCars()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarImagesAndNumberOfCars);
			else
				return CreateSqlCommand(procedureAllCarImagesAndNumberOfCars);
		}

		static public MySqlCommand GetNumberOfCarWithImage(string carPicture)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPicture(carPicture, queryNumberOfCarWithImage);
			else
				return CreateSqlCommandPicture(carPicture, procedureNumberOfCarWithImage);
		}

		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(CarModel car, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", car.carNumber);
			command.Parameters.AddWithValue("@carTypeID", car.carTypeID);
			command.Parameters.AddWithValue("@carKm", car.carKm);
			command.Parameters.AddWithValue("@carPicture", car.carPicture);
			command.Parameters.AddWithValue("@carInShape", car.carInShape);
			command.Parameters.AddWithValue("@carAvaliable", car.carAvaliable);
			command.Parameters.AddWithValue("@carBranchID", car.carBranchID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandNumber(string carNumber, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}

		static private MySqlCommand CreateSqlCommandPicture(string carPicture, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carPicture", carPicture);

			return command;
		}
	}
}
