using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class CarTypeStringsMySql
	{
		static private string queryAllCarTypesString = "SELECT * from ALLCARTYPES;";
		static private string queryAllCarTypesIdsString = "SELECT carTypeID, thisCarType from ALLCARTYPES;";
		static private string queryCarTypeByIdString = "SELECT * from ALLCARTYPES WHERE carTypeID = @carTypeID;";
		static private string queryCarTypePost = "INSERT INTO ALLCARTYPES (thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear) VALUES (@thisCarType, @carFirm, @carModel, @carDayPrice, @carLatePrice, @carYear, @carGear); SELECT * from ALLCARTYPES WHERE carTypeID = SCOPE_IDENTITY();";
		static private string queryCarTypeUpdate = "UPDATE ALLCARTYPES SET thisCarType = @thisCarType, carFirm = @carFirm, carModel=@carModel, carDayPrice = @carDayPrice, carLatePrice = @carLatePrice, carYear = @carYear, carGear = @carGear WHERE carTypeID = @carTypeID; SELECT * from ALLCARTYPES WHERE carTypeID = @carTypeID;";
		static private string queryCarTypeDeleteById = "DELETE FROM ALLCARTYPES WHERE carTypeID = @carTypeID;";
		static private string queryCarTypeDeleteByType = "DELETE FROM ALLCARTYPES WHERE thisCarType = @thisCarType;";


		static private string procedureAllCarTypesString = "CALL `rentcar`.`GetAllCarTypes`();";
		static private string procedureAllCarTypesIdsString = "CALL `rentcar`.`GetAllCarTypesIds`();";
		static private string procedureCarTypeByIdString = "CALL `rentcar`.`GetOneCarType`(@carTypeID);";
		static private string procedureCarTypePost = "CALL `rentcar`.`AddCarType`(@thisCarType, @carFirm, @carModel, @carDayPrice, @carLatePrice, @carYear, @carGear);";
		static private string procedureCarTypeUpdate = "CALL `rentcar`.`UpdateCarType`(@thisCarType, @carFirm, @carModel, @carDayPrice, @carLatePrice, @carYear, @carGear, @carTypeID);";
		static private string procedureCarTypeDeleteById = "CALL `rentcar`.`DeleteCarTypeById`(@carTypeID);";
		static private string procedureCarTypeDeleteByType = "CALL `rentcar`.`DeleteCarTypeByType`(@thisCarType);";

		static public MySqlCommand GetAllCarTypes()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarTypesString);
			else
				return CreateSqlCommand(procedureAllCarTypesString);
		}

		static public MySqlCommand GetAllCarTypesIds()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryAllCarTypesIdsString);
			else
				return CreateSqlCommand(procedureAllCarTypesIdsString);
		}

		static public MySqlCommand GetOneCarType(int typeId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(typeId, queryCarTypeByIdString);
			else
				return CreateSqlCommand(typeId, procedureCarTypeByIdString);
		}

		static public MySqlCommand AddCarType(CarTypeModel carTypeModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carTypeModel, queryCarTypePost);
			else
				return CreateSqlCommand(carTypeModel, procedureCarTypePost);
		}

		static public MySqlCommand UpdateCarType(CarTypeModel carTypeModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carTypeModel, queryCarTypeUpdate);
			else
				return CreateSqlCommand(carTypeModel, procedureCarTypeUpdate);
		}

		static public MySqlCommand DeleteCarTypeByType(string carType)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carType, queryCarTypeDeleteByType);
			else
				return CreateSqlCommand(carType, procedureCarTypeDeleteByType);

		}

		static public MySqlCommand DeleteCarTypeById(int carTypeId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(carTypeId, queryCarTypeDeleteById);
			else
				return CreateSqlCommand(carTypeId, procedureCarTypeDeleteById);
		}



		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(CarTypeModel carType, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carTypeId", carType.carTypeId);
			command.Parameters.AddWithValue("@thisCarType", carType.carType);
			command.Parameters.AddWithValue("@carFirm", carType.carFirm);
			command.Parameters.AddWithValue("@carModel", carType.carModel);
			command.Parameters.AddWithValue("@carDayPrice", carType.carDayPrice);
			command.Parameters.AddWithValue("@carLatePrice", carType.carLatePrice);
			command.Parameters.AddWithValue("@carYear", carType.carYear);
			command.Parameters.AddWithValue("@carGear", carType.carGear);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(int carTypeId, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@carTypeId", carTypeId);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(string thisCarType, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@thisCarType", thisCarType);

			return command;
		}
	}
}
