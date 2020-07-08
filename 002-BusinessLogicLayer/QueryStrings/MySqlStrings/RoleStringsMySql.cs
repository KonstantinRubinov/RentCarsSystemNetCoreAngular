using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class RoleStringsMySql
	{
		static private string queryRoleString = "SELECT * from Roles;";
		static private string queryRoleByLevelString = "SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByRoleString = "SELECT * from Roles WHERE userRole = @userRole;";
		static private string queryRolePost = "INSERT INTO Roles (userLevel, userRole) VALUES (@userLevel, @userRole); SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleUpdate = "UPDATE Roles SET userLevel = @userLevel, userRole = @userRole WHERE userLevel = @userLevel; SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByLevelDelete = "DELETE FROM Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByRoleDelete = "DELETE FROM Roles WHERE userRole = @userRole;";


		static private string procedureRoleString = "CALL `rentcar`.`GetAllRoles`();";
		static private string procedureRoleByLevelString = "CALL `rentcar`.`GetOneRole`(@userLevel);";
		static private string procedureRoleByRoleString = "CALL `rentcar`.`GetOneRoleByRole`(@userRole);";
		static private string procedureRolePost = "CALL `rentcar`.`AddRole`(@userLevel, @userRole);";
		static private string procedureRoleUpdate = "CALL `rentcar`.`UpdateRole`(@userLevel, @userRole);";
		static private string procedureRoleByLevelDelete = "CALL `rentcar`.`DeleteRoleByLevel`(@userLevel);";
		static private string procedureRoleByRoleDelete = "CALL `rentcar`.`DeleteRoleByRole`(@userRole);";


		static public MySqlCommand GetAllRoles()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryRoleString);
			else
				return CreateSqlCommand(procedureRoleString);
		}

		static public MySqlCommand GetOneRoleByLevel(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userLevel, queryRoleByLevelString);
			else
				return CreateSqlCommandId(userLevel, procedureRoleByLevelString);
		}

		static public MySqlCommand GetOneRoleByRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userRole, queryRoleByRoleString);
			else
				return CreateSqlCommandId(userRole, procedureRoleByRoleString);
		}

		static public MySqlCommand AddRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(roleModel, queryRolePost);
			else
				return CreateSqlCommand(roleModel, procedureRolePost);
		}

		static public MySqlCommand UpdateRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(roleModel, queryRoleUpdate);
			else
				return CreateSqlCommand(roleModel, procedureRoleUpdate);
		}


		static public MySqlCommand DeleteRoleByRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userRole, queryRoleByRoleDelete);
			else
				return CreateSqlCommandId(userRole, procedureRoleByRoleDelete);
		}

		static public MySqlCommand DeleteRoleByLevel(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userLevel, queryRoleByLevelDelete);
			else
				return CreateSqlCommandId(userLevel, procedureRoleByLevelDelete);
		}


		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(RoleModel roleModel, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userLevel", roleModel.userLevel);
			command.Parameters.AddWithValue("@userRole", roleModel.userRole);

			return command;
		}

		static private MySqlCommand CreateSqlCommandId(string userRole, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userRole", userRole);

			return command;
		}

		static private MySqlCommand CreateSqlCommandId(int userLevel, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userLevel", userLevel);

			return command;
		}
	}
}
