using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class RoleStringsSql
	{
		static private string queryRoleString = "SELECT * from Roles;";
		static private string queryRoleByLevelString = "SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByRoleString = "SELECT * from Roles WHERE userRole = @userRole;";
		static private string queryRolePost = "INSERT INTO Roles (userLevel, userRole) VALUES (@userLevel, @userRole); SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleUpdate = "UPDATE Roles SET userLevel = @userLevel, userRole = @userRole WHERE userLevel = @userLevel; SELECT * from Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByLevelDelete = "DELETE FROM Roles WHERE userLevel = @userLevel;";
		static private string queryRoleByRoleDelete = "DELETE FROM Roles WHERE userRole = @userRole;";

		static private string procedureRoleString = "EXEC GetAllRoles;";
		static private string procedureRoleByLevelString = "EXEC GetOneRole @userLevel;";
		static private string procedureRoleByRoleString = "EXEC GetOneRoleByRole @userRole;";
		static private string procedureRolePost = "EXEC AddRole @userLevel, @userRole;";
		static private string procedureRoleUpdate = "EXEC UpdateRole @userLevel, @userRole;";
		static private string procedureRoleByLevelDelete = "EXEC DeleteRoleByLevel @userLevel;";
		static private string procedureRoleByRoleDelete = "EXEC DeleteRoleByRole @userRole;";

		static public SqlCommand GetAllRoles()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryRoleString);
			else
				return CreateSqlCommand(procedureRoleString);
		}

		static public SqlCommand GetOneRoleByLevel(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userLevel, queryRoleByLevelString);
			else
				return CreateSqlCommandId(userLevel, procedureRoleByLevelString);
		}

		static public SqlCommand GetOneRoleByRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userRole, queryRoleByRoleString);
			else
				return CreateSqlCommandId(userRole, procedureRoleByRoleString);
		}

		static public SqlCommand AddRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(roleModel, queryRolePost);
			else
				return CreateSqlCommand(roleModel, procedureRolePost);
		}

		static public SqlCommand UpdateRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(roleModel, queryRoleUpdate);
			else
				return CreateSqlCommand(roleModel, procedureRoleUpdate);
		}


		static public SqlCommand DeleteRoleByRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userRole, queryRoleByRoleDelete);
			else
				return CreateSqlCommandId(userRole, procedureRoleByRoleDelete);
		}

		static public SqlCommand DeleteRoleByLevel(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(userLevel, queryRoleByLevelDelete);
			else
				return CreateSqlCommandId(userLevel, procedureRoleByLevelDelete);
		}







		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommand(RoleModel roleModel, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@userLevel", roleModel.userLevel);
			command.Parameters.AddWithValue("@userRole", roleModel.userRole);

			return command;
		}

		static private SqlCommand CreateSqlCommandId(string userRole, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@userRole", userRole);

			return command;
		}

		static private SqlCommand CreateSqlCommandId(int userLevel, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@userLevel", userLevel);

			return command;
		}
	}
}
