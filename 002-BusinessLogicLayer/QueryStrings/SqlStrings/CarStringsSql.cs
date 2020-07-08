using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class CarStringsSql
	{
		static private string queryAllCarsString = "SELECT * from Cars;";
		static private string queryCarByNumberString = "SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarPost = "INSERT INTO Cars (carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID) VALUES (@carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID); SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarUpdate = "UPDATE Cars SET carKm = @carKm, carPicture=@carPicture, carInShape = @carInShape, carAvaliable = @carAvaliable, carNumber = @carNumber, carBranchID = @carBranchID, carTypeID = @carTypeID WHERE carNumber = @carNumber; SELECT * from Cars WHERE carNumber = @carNumber;";
		static private string queryCarDelete = "SELECT carPicture from Cars WHERE carNumber = @carNumber; DELETE FROM Cars WHERE carNumber = @carNumber;";
		static private string queryAllCarImagesAndNumberOfCars = "SELECT carPicture, COUNT(carNumber) FROM Cars GROUP BY carPicture;";
		static private string queryNumberOfCarWithImage = "SELECT carPicture, COUNT(carNumber) FROM Cars WHERE carPicture = @carPicture GROUP BY carPicture;";

		static private string procedureAllCarsString = "EXEC GetAllCars;";
		static private string procedureCarByNumberString = "EXEC GetOneCarByNumber @carNumber;";
		static private string procedureCarPost = "EXEC AddCar @carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID;";
		static private string procedureCarUpdate = "EXEC UpdateCar @carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID;";
		static private string procedureCarDelete = "EXEC DeleteCar @carNumber;";
		static private string procedureAllCarImagesAndNumberOfCars = "EXEC GetAllCarImagesAndNumberOfCars;";
		static private string procedureNumberOfCarWithImage = "EXEC GetNumberOfCarWithImage @carPicture;";


		static public SqlCommand GetAllCars()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarsString);
			else
				return CreateSqlCommand(procedureAllCarsString);
		}

		static public SqlCommand GetOneCarByNumber(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandNumber(carNumber, queryCarByNumberString);
			else
				return CreateSqlCommandNumber(carNumber, procedureCarByNumberString);
		}

		static public SqlCommand AddCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carModel, queryCarPost);
			else
				return CreateSqlCommand(carModel, procedureCarPost);
		}

		static public SqlCommand UpdateCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carModel, queryCarUpdate);
			else
				return CreateSqlCommand(carModel, procedureCarUpdate);
		}

		static public SqlCommand DeleteCar(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandNumber(carNumber, queryCarDelete);
			else
				return CreateSqlCommandNumber(carNumber, procedureCarDelete);
		}

		static public SqlCommand GetAllCarImagesAndNumberOfCars()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarImagesAndNumberOfCars);
			else
				return CreateSqlCommand(procedureAllCarImagesAndNumberOfCars);
		}

		static public SqlCommand GetNumberOfCarWithImage(string carPicture)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPicture(carPicture, queryNumberOfCarWithImage);
			else
				return CreateSqlCommandPicture(carPicture, procedureNumberOfCarWithImage);
		}

		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommand(CarModel car, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", car.carNumber);
			command.Parameters.AddWithValue("@carTypeID", car.carTypeID);
			command.Parameters.AddWithValue("@carKm", car.carKm);
			command.Parameters.AddWithValue("@carPicture", car.carPicture);
			command.Parameters.AddWithValue("@carInShape", car.carInShape);
			command.Parameters.AddWithValue("@carAvaliable", car.carAvaliable);
			command.Parameters.AddWithValue("@carBranchID", car.carBranchID);

			return command;
		}

		static private SqlCommand CreateSqlCommandNumber(string carNumber, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@carNumber", carNumber);

			return command;
		}

		static private SqlCommand CreateSqlCommandPicture(string carPicture, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@carPicture", carPicture);

			return command;
		}
	}
}
