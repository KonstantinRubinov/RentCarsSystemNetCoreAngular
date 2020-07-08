using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class RoleModel
	{
		private string _userLevelId;

		private int _userLevel = 0;
		private string _userRole;

		public RoleModel(int tmpUserLevel, string tmpUserRole)
		{
			userLevel = tmpUserLevel;
			userRole = tmpUserRole;
		}

		public RoleModel()
		{

		}

		public const string Admin = "Admin";
		public const string Manager = "Manager";
		public const string User = "User";
		public const string Guest = "Guest";

		//public const int Admin = 4;
		//public const int Manager = 2;
		//public const int User = 1;
		//public const int Guest = 0;

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string userLevelId
		{
			get { return _userLevelId; }
			set { _userLevelId = value; }
		}

		[DataMember]
		public string userRole
		{
			get { return _userRole; }
			set { _userRole = value; }
		}

		[DataMember]
		public int userLevel
		{
			get { return _userLevel; }
			set { _userLevel = value; }
		}

		public override string ToString()
		{
			return
				userLevel + " " +
				userRole + " " +
				userLevelId;
		}
		public static RoleModel ToObject(DataRow reader)
		{
			RoleModel roleModel = new RoleModel();
			roleModel.userLevel = int.Parse(reader[0].ToString());
			roleModel.userRole = reader[1].ToString();

			Debug.WriteLine("roleModel: " + roleModel.ToString());
			return roleModel;
		}
	}
}
