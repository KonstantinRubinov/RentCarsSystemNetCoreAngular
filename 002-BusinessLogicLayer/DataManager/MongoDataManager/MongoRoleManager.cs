using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	public class MongoRoleManager : IRoleRepository
	{
		private readonly IMongoCollection<RoleModel> _roles;

		public MongoRoleManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_roles = database.GetCollection<RoleModel>(settings.RolesCollectionName);
		}

		public MongoRoleManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_roles = database.GetCollection<RoleModel>(ConnectionStrings.RolesCollectionName);
		}


		public List<RoleModel> GetAllRoles()
		{
			return _roles.Find(role => true).Project(r => new RoleModel
			{
				userLevel = r.userLevel,
				userRole = r.userRole
			}).ToList();
		}

		public RoleModel GetOneRole(int userLevel)
		{
			if (userLevel < 0)
				throw new ArgumentOutOfRangeException();

			return _roles.Find<RoleModel>(Builders<RoleModel>.Filter.Eq(role => role.userLevel, userLevel)).Project(r => new RoleModel
			{
				userLevel = r.userLevel,
				userRole = r.userRole
			}).FirstOrDefault();
		}

		public RoleModel GetOneRole(string userRole)
		{
			if (userRole.Equals(String.Empty))
				throw new ArgumentOutOfRangeException();

			return _roles.Find<RoleModel>(Builders<RoleModel>.Filter.Eq(role => role.userRole, userRole)).Project(r => new RoleModel
			{
				userLevel = r.userLevel,
				userRole = r.userRole
			}).FirstOrDefault();
		}

		public RoleModel AddRole(RoleModel roleModel)
		{
			if (GetOneRole(roleModel.userLevel) == null)
			{
				_roles.InsertOne(roleModel);
			}
			RoleModel tmpRoleModel = GetOneRole(roleModel.userLevel);
			return tmpRoleModel;
		}

		public RoleModel UpdateRole(RoleModel roleModel)
		{
			_roles.ReplaceOne(role => role.userLevel == roleModel.userLevel, roleModel);
			RoleModel tmpRoleModel = GetOneRole(roleModel.userLevel);
			return tmpRoleModel;
		}

		public int DeleteRole(string userRole)
		{
			_roles.DeleteOne(role => role.userRole.Equals(userRole));
			return 1;
		}

		public int DeleteRole(int userLevel)
		{
			_roles.DeleteOne(role => role.userLevel == userLevel);
			return 1;
		}
	}
}
