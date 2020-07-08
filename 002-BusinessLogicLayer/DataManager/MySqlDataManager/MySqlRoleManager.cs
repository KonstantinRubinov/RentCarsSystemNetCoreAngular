using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlRoleManager : MySqlDataBase, IRoleRepository
	{
		public List<RoleModel> GetAllRoles()
		{
			DataTable dt = new DataTable();
			List<RoleModel> arrRoles = new List<RoleModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsMySql.GetAllRoles());
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsMySql.GetOneRoleByLevel(userLevel));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsMySql.GetOneRoleByRole(userRole));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsMySql.AddRole(roleModel));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(RoleStringsMySql.UpdateRole(roleModel));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(RoleStringsMySql.DeleteRoleByRole(userRole));
			}
			return i;
		}

		public int DeleteRole(int userLevel)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(RoleStringsMySql.DeleteRoleByLevel(userLevel));
			}
			return i;
		}
	}
}
