using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface IRoleRepository
	{
		List<RoleModel> GetAllRoles();
		RoleModel GetOneRole(int userLevel);
		RoleModel GetOneRole(string userRole);
		RoleModel AddRole(RoleModel roleModel);
		RoleModel UpdateRole(RoleModel roleModel);
		int DeleteRole(string userRole);
		int DeleteRole(int userLevel);
	}
}
