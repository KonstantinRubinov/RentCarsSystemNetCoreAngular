using MySql.Data.MySqlClient;

namespace RentCarsServerCore
{
	static public class BranchStringsMySql
	{
		static private string queryBranchNamesIdsString = "SELECT branchID, branchName from Branches;";
		static private string queryBranchString = "SELECT * from Branches;";
		static private string queryBranchByIdString = "SELECT * from Branches WHERE branchID = @branchID;";
		static private string queryBranchPost = "INSERT INTO Branches (branchAddress, branchLat, branchLng, branchName, branchID) VALUES (@branchAddress, @branchLat, @branchLng, @branchName, @branchID); SELECT * FROM Branches WHERE branchID = SCOPE_IDENTITY();";
		static private string queryBranchUpdate = "UPDATE Branches SET branchAddress = @branchAddress, branchLat=@branchLat, branchLng = @branchLng, branchName = @branchName WHERE branchID = @branchID; SELECT * from Branches WHERE branchID = @branchID;";
		static private string queryBranchDelete = "DELETE FROM Branches WHERE branchID = @branchID;";
		
		
		static private string procedureBranchNamesIdsString = "CALL `rentcar`.`GetAllBranchesNamesIds`();";
		static private string procedureBranchString = "CALL `rentcar`.`GetAllBranches`();";
		static private string procedureBranchByIdString = "CALL `rentcar`.`GetOneBranchById`(@branchID);";
		static private string procedureBranchPost = "CALL `rentcar`.`AddBranch`(@branchAddress, @branchLat, @branchLng, @branchName);";
		static private string procedureBranchUpdate = "CALL `rentcar`.`UpdateBranch`(@branchAddress, @branchLat, @branchLng, @branchName, @branchID);";
		static private string procedureBranchDelete = "CALL `rentcar`.`DeleteBranch`(@branchID);";

		static public MySqlCommand GetAllBranchesNamesIds()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryBranchNamesIdsString);
			else
				return CreateSqlCommand(procedureBranchNamesIdsString);
		}

		static public MySqlCommand GetAllBranches()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryBranchString);
			else
				return CreateSqlCommand(procedureBranchString);
		}

		static public MySqlCommand GetOneBranchById(int branchID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(branchID, queryBranchByIdString);
			else
				return CreateSqlCommandId(branchID, procedureBranchByIdString);
		}

		static public MySqlCommand AddBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(branchModel, queryBranchPost);
			else
				return CreateSqlCommand(branchModel, procedureBranchPost);
		}

		static public MySqlCommand UpdateBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(branchModel, queryBranchUpdate);
			else
				return CreateSqlCommand(branchModel, procedureBranchUpdate);
		}

		static public MySqlCommand DeleteBranch(int branchID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(branchID, queryBranchDelete);
			else
				return CreateSqlCommandId(branchID, procedureBranchDelete);
		}

		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(BranchModel branch, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@branchID", branch.branchID);
			command.Parameters.AddWithValue("@branchName", branch.branchName);
			command.Parameters.AddWithValue("@branchAddress", branch.branchAddress);
			command.Parameters.AddWithValue("@branchLat", branch.branchLat);
			command.Parameters.AddWithValue("@branchLng", branch.branchLng);

			return command;
		}

		static private MySqlCommand CreateSqlCommandId(int branchID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@branchID", branchID);

			return command;
		}
	}
}
