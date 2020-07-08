using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlBranchManager : MySqlDataBase, IBranchRepository
	{
		public List<BranchModel> GetAllBranchesNamesIds()
		{
			DataTable dt = new DataTable();
			List<BranchModel> arrBranches = new List<BranchModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsMySql.GetAllBranchesNamesIds());
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrBranches.Add(BranchModel.ToObject(ms));
			}

			return arrBranches;
		}

		public List<BranchModel> GetAllBranches()
		{
			DataTable dt = new DataTable();
			List<BranchModel> arrBranches = new List<BranchModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsMySql.GetAllBranches());
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrBranches.Add(BranchModel.ToObject(ms));
			}

			return arrBranches;
		}


		public BranchModel GetOneBranch(int branchID)
		{
			if (branchID < 0)
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			BranchModel branchModelSql = new BranchModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsMySql.GetOneBranchById(branchID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				branchModelSql = BranchModel.ToObject(ms);
			}

			return branchModelSql;
		}

		public BranchModel AddBranch(BranchModel branchModel)
		{
			DataTable dt = new DataTable();
			BranchModel branchModelSql = new BranchModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsMySql.AddBranch(branchModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				branchModelSql = BranchModel.ToObject(ms);
			}

			return branchModelSql;
		}

		public BranchModel UpdateBranch(BranchModel branchModel)
		{
			DataTable dt = new DataTable();
			BranchModel branchModelSql = new BranchModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsMySql.UpdateBranch(branchModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				branchModelSql = BranchModel.ToObject(ms);
			}

			return branchModelSql;
		}

		public int DeleteBranch(int branchID)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(BranchStringsMySql.DeleteBranch(branchID));
			}
			return i;
		}
	}
}
