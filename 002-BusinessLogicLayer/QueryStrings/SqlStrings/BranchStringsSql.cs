using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class BranchStringsSql
	{
		static private string queryBranchNamesIdsString = "SELECT branchID, branchName from Branches;";
		static private string queryBranchString = "SELECT * from Branches;";
		static private string queryBranchByIdString = "SELECT * from Branches WHERE branchID = @branchID;";
		static private string queryBranchPost = "INSERT INTO Branches (branchAddress, branchLat, branchLng, branchName, branchID) VALUES (@branchAddress, @branchLat, @branchLng, @branchName, @branchID); SELECT * FROM Branches WHERE branchID = SCOPE_IDENTITY();";
		static private string queryBranchUpdate = "UPDATE Branches SET branchAddress = @branchAddress, branchLat=@branchLat, branchLng = @branchLng, branchName = @branchName WHERE branchID = @branchID; SELECT * from Branches WHERE branchID = @branchID;";
		static private string queryBranchDelete = "DELETE FROM Branches WHERE branchID = @branchID;";

		static private string procedureBranchNamesIdsString = "EXEC GetAllBranchesNamesIds;";
		static private string procedureBranchString = "EXEC GetAllBranches;";
		static private string procedureBranchByIdString = "EXEC GetOneBranchById @branchID;";
		static private string procedureBranchPost = "EXEC AddBranch @branchAddress, @branchLat, @branchLng, @branchName;";
		static private string procedureBranchUpdate = "EXEC UpdateBranch @branchAddress, @branchLat, @branchLng, @branchName, @branchID;";
		static private string procedureBranchDelete = "EXEC DeleteBranch @branchID;";

		static public SqlCommand GetAllBranchesNamesIds()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryBranchNamesIdsString);
			else
				return CreateSqlCommand(procedureBranchNamesIdsString);
		}

		static public SqlCommand GetAllBranches()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryBranchString);
			else
				return CreateSqlCommand(procedureBranchString);
		}

		static public SqlCommand GetOneBranchById(int branchID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(branchID, queryBranchByIdString);
			else
				return CreateSqlCommandId(branchID, procedureBranchByIdString);
		}

		static public SqlCommand AddBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(branchModel, queryBranchPost);
			else
				return CreateSqlCommand(branchModel, procedureBranchPost);
		}

		static public SqlCommand UpdateBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(branchModel, queryBranchUpdate);
			else
				return CreateSqlCommand(branchModel, procedureBranchUpdate);
		}

		static public SqlCommand DeleteBranch(int branchID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(branchID, queryBranchDelete);
			else
				return CreateSqlCommandId(branchID, procedureBranchDelete);
		}

		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommand(BranchModel branch, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@branchID", branch.branchID);
			command.Parameters.AddWithValue("@branchName", branch.branchName);
			command.Parameters.AddWithValue("@branchAddress", branch.branchAddress);
			command.Parameters.AddWithValue("@branchLat", branch.branchLat);
			command.Parameters.AddWithValue("@branchLng", branch.branchLng);

			return command;
		}

		static private SqlCommand CreateSqlCommandId(int branchID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@branchID", branchID);

			return command;
		}
	}
}
