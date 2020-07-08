using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlBranchManager : SqlDataBase, IBranchRepository
	{
		public List<BranchModel> GetAllBranchesNamesIds()
		{
			DataTable dt = new DataTable();
			List<BranchModel> arrBranches = new List<BranchModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsSql.GetAllBranchesNamesIds());
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsSql.GetAllBranches());
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsSql.GetOneBranchById(branchID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsSql.AddBranch(branchModel));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(BranchStringsSql.UpdateBranch(branchModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(BranchStringsSql.DeleteBranch(branchID));
			}
			return i;
		}
	}
}
