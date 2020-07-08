using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class UserStringsMySql
	{
		static private string queryUsersString = "SELECT * from Users;";
		static private string queryUserByIdString = "SELECT * from Users WHERE userID = @userID;";
		static private string queryUserByNameString = "SELECT * from Users WHERE userNickName = @userNickName;";
		static private string queryUserByLoginString = "SELECT * from Users WHERE userNickName = @userNickName and userPassword=@userPassword;";
		static private string queryUsersPost = "INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userLevel) VALUES (@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userLevel); SELECT * from Users WHERE userID = @userID;";
		static private string queryUsersUpdate = "UPDATE Users SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userNickName = @userNickName, userBirthDate = @userBirthDate, userGender = @userGender, userEmail = @userEmail, userPassword = @userPassword, userPicture = @userPicture, userLevel = @userLevel where userID=@userID; SELECT * from Users WHERE userID = @userID;";
		static private string queryUsersDelete = "DELETE FROM Users WHERE userID=@userID";
		static private string queryUsersNamePassword = "SELECT userNickName, userLevel, userPicture from Users WHERE userNickName=@userNickName and userPassword=@userPassword";
		static private string queryUsersNameLevel = "SELECT userNickName, userLevel from Users WHERE userNickName=@userNickName and userLevel=@userLevel";
		static private string queryUsersIdByPassword = "SELECT userID from Users WHERE userPassword=@userPassword";
		static private string queryUsersIfNameExists = "SELECT COUNT(1) FROM Users WHERE userNickName = @userNickName;";
		static private string queryUsersPictureUpdate = "UPDATE Users SET userPicture = @userPicture where userID=@userID; SELECT * FROM Users where userID=@userID;";
		static private string queryUserForMessageString = "SELECT userFirstName, userLastName, userEmail from Users WHERE userID = @userID;";



		static private string procedureUsersString = "CALL `rentcar`.`GetAllUsers`();";
		static private string procedureUserByIdString = "CALL `rentcar`.`GetOneUserById`(@userID);";
		static private string procedureUserByNameString = "CALL `rentcar`.`GetOneUserByName`(@userNickName);";
		static private string procedureUserByLoginString = "CALL  `rentcar`.`GetOneUserByLogin`(@userNickName, @userPassword);";
		static private string procedureUsersPost = "CALL `rentcar`.`AddUser`(@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userLevel);";
		static private string procedureUsersUpdate = "CALL `rentcar`.`UpdateUser`(@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userLevel);";
		static private string procedureUsersDelete = "CALL `rentcar`.`DeleteUser`(@userID);";
		static private string procedureUsersNamePassword = "CALL `rentcar`.`ReturnUserByNamePassword`(@userNickName, @userPassword);";
		static private string procedureUsersNameLevel = "CALL `rentcar`.`ReturnUserByNameLevel`(@userNickName, @userLevel);";
		static private string procedureUsersIdByPassword = "CALL `rentcar`.`ReturnUserIdByUserPass`(@userPassword);";
		static private string procedureUsersIfNameExists = "CALL `rentcar`.`IsNameTaken`(@userNickName);";
		static private string procedureUsersPictureUpdate = "CALL `rentcar`.`UploadUserImage`(@userID, @userPicture);";
		static private string procedureUserForMessageString = "CALL `rentcar`.`GetOneUserForMessageById`(@userID);";

		static public MySqlCommand GetAllUsers()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryUsersString);
			else
				return CreateSqlCommand(procedureUsersString);
		}

		static public MySqlCommand GetOneUserById(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userID, queryUserByIdString);
			else
				return CreateSqlCommandId(userID, procedureUserByIdString);
		}

		static public MySqlCommand GetOneUserByName(string userNickName)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandName(userNickName, queryUserByNameString);
			else
				return CreateSqlCommandName(userNickName, procedureUserByNameString);
		}

		static public MySqlCommand GetOneUserByLogin(string userNickName, string password)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandByLogin(userNickName, password, queryUserByLoginString);
			else
				return CreateSqlCommandByLogin(userNickName, password, procedureUserByLoginString);
		}

		static public MySqlCommand AddUser(UserModel userModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userModel, queryUsersPost);
			else
				return CreateSqlCommand(userModel, procedureUsersPost);
		}

		static public MySqlCommand UpdateUser(UserModel userModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userModel, queryUsersUpdate);
			else
				return CreateSqlCommand(userModel, procedureUsersUpdate);
		}

		static public MySqlCommand DeleteUser(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userID, queryUsersDelete);
			else
				return CreateSqlCommandId(userID, procedureUsersDelete);
		}

		static public MySqlCommand ReturnUserByNamePassword(LoginModel checkUser)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(checkUser, queryUsersNamePassword);
			else
				return CreateSqlCommand(checkUser, procedureUsersNamePassword);
		}

		static public MySqlCommand ReturnUserByNameLevel(string username, int userLevel = 0)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(username, userLevel, queryUsersNameLevel);
			else
				return CreateSqlCommand(username, userLevel, procedureUsersNameLevel);
		}

		static public MySqlCommand ReturnUserIdByUserPass(string userPass)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPass(userPass, queryUsersIdByPassword);
			else
				return CreateSqlCommandPass(userPass, procedureUsersIdByPassword);
		}

		static public MySqlCommand IsNameTaken(string name)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandName(name, queryUsersIfNameExists);
			else
				return CreateSqlCommandName(name, procedureUsersIfNameExists);
		}

		static public MySqlCommand UploadUserImage(string id, string img)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPicture(id, img, queryUsersPictureUpdate);
			else
				return CreateSqlCommandPicture(id, img, procedureUsersPictureUpdate);
		}

		static public MySqlCommand GetOneUserForMessageById(string id)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandUserForMessage(id, queryUserForMessageString);
			else
				return CreateSqlCommandUserForMessage(id, procedureUserForMessageString);
		}



		static private MySqlCommand CreateSqlCommand(UserModel user, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", user.userID);
			command.Parameters.AddWithValue("@userFirstName", user.userFirstName);
			command.Parameters.AddWithValue("@userLastName", user.userLastName);
			command.Parameters.AddWithValue("@userNickName", user.userNickName);
			command.Parameters.AddWithValue("@userBirthDate", user.userBirthDate);
			command.Parameters.AddWithValue("@userGender", user.userGender);
			command.Parameters.AddWithValue("@userEmail", user.userEmail);
			command.Parameters.AddWithValue("@userPassword", user.userPassword);
			command.Parameters.AddWithValue("@userPicture", user.userPicture);
			command.Parameters.AddWithValue("@userLevel", user.userLevel);

			return command;
		}



		static private MySqlCommand CreateSqlCommandId(string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandName(string userNickName, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userNickName", userNickName);

			return command;
		}

		static private MySqlCommand CreateSqlCommandByLogin(string userNickName, string userPassword, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userNickName", userNickName);
			command.Parameters.AddWithValue("@userPassword", userPassword);

			return command;
		}



		static private MySqlCommand CreateSqlCommand(LoginModel user, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", user.userNickName);
			command.Parameters.AddWithValue("@userPassword", user.userPassword);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(string userNickName, int userLevel, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", userNickName);
			command.Parameters.AddWithValue("@userLevel", userLevel);
			return command;
		}

		static private MySqlCommand CreateSqlCommandPicture(string userID, string userPicture, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userID", userID);
			command.Parameters.AddWithValue("@userPicture", userPicture);
			return command;
		}

		static private MySqlCommand CreateSqlCommandPass(string userPassword, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userPassword", userPassword);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommandUserForMessage(string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userID", userID);
			return command;
		}
	}
}
