using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlRoleManager : SqlDataBase, IRoleRepository
	{
		public List<RoleModel> GetAllRoles()
		{
			DataTable dt = new DataTable();
			List<RoleModel> arrRoles = new List<RoleModel>();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsSql.GetAllRoles());
			}
			foreach (DataRow ms in dt.Rows)
			{
				arrRoles.Add(RoleModel.ToObject(ms));
			}

			return arrRoles;
		}

		public RoleModel GetOneRole(int userLevel)
		{
			if (userLevel < 0)
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			RoleModel roleModelSql = new RoleModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsSql.GetOneRoleByLevel(userLevel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				roleModelSql = RoleModel.ToObject(ms);
			}

			return roleModelSql;
		}

		public RoleModel GetOneRole(string userRole)
		{
			if (userRole == null || userRole.Equals(String.Empty) || userRole.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			RoleModel roleModelSql = new RoleModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsSql.GetOneRoleByRole(userRole));
			}
			foreach (DataRow ms in dt.Rows)
			{
				roleModelSql = RoleModel.ToObject(ms);
			}

			return roleModelSql;
		}


		public RoleModel AddRole(RoleModel roleModel)
		{
			DataTable dt = new DataTable();
			RoleModel roleModelSql = new RoleModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsSql.AddRole(roleModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				roleModelSql = RoleModel.ToObject(ms);
			}

			return roleModelSql;
		}


		public RoleModel UpdateRole(RoleModel roleModel)
		{
			DataTable dt = new DataTable();
			RoleModel roleModelSql = new RoleModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsSql.UpdateRole(roleModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				roleModelSql = RoleModel.ToObject(ms);
			}

			return roleModelSql;
		}

		public int DeleteRole(string userRole)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(RoleStringsSql.DeleteRoleByRole(userRole));
			}
			return i;
		}

		public int DeleteRole(int userLevel)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(RoleStringsSql.DeleteRoleByLevel(userLevel));
			}
			return i;
		}
	}
}
