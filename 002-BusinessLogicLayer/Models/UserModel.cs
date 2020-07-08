using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class UserModel
	{
		private string _userID;
		private string _userFirstName;
		private string _userLastName;
		private string _userNickName;
		private string _userPassword;
		private string _userEmail;
		private string _userGender;
		private DateTime? _userBirthDate;
		private string _userPicture;
		private int _userLevel = 0;
		private string _userRole;
		private string _userImage;
		private string _userToken;

		[DataMember]
		[Required(ErrorMessage = "Missing user id.")]
		[PossibleID]
		public string userID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user first name.")]
		[StringLength(40, ErrorMessage = "User first name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "User first name mast be minimum 2 chars.")]
		[RegularExpression("^[A-Z].*$", ErrorMessage = "User first name must start with a capital letter.")]
		public string userFirstName
		{
			get { return _userFirstName; }
			set { _userFirstName = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user last name.")]
		[StringLength(40, ErrorMessage = "User last name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "User first last mast be minimum 2 chars.")]
		[RegularExpression("^[A-Z].*$", ErrorMessage = "User last name must start with a capital letter.")]
		public string userLastName
		{
			get { return _userLastName; }
			set { _userLastName = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user nick name.")]
		[StringLength(40, ErrorMessage = "User nick name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "User first nick mast be minimum 2 chars.")]
		[UniqueUserName]
		public string userNickName
		{
			get { return _userNickName; }
			set { _userNickName = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user password.")]
		public string userPassword
		{
			get { return _userPassword; }
			set { _userPassword = value; }
		}

		const string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
		[Required(ErrorMessage = "Missing user email.")]
		[RegularExpression(pattern, ErrorMessage = "User mail wrong.")]
		[DataMember]
		public string userEmail
		{
			get { return _userEmail; }
			set { _userEmail = value; }
		}


		[DataMember]
		[Required(ErrorMessage = "Missing user gender.")]
		[PossibleGender]
		public string userGender
		{
			get { return _userGender; }
			set { _userGender = value; }
		}



		[DataMember]
		public DateTime? userBirthDate
		{
			get { return _userBirthDate; }
			set { _userBirthDate = value; }
		}

		[DataMember]
		public string userPicture
		{
			get { return _userPicture; }
			set { _userPicture = value; }
		}

		[DataMember]
		public int userLevel
		{
			get
			{
				return _userLevel;
			}
			set
			{
				_userLevel = value;
				switch (value)
				{
					case 0:
						_userRole = "Guest";
						break;
					case 1:
						_userRole = "User";
						break;
					case 2:
						_userRole = "Manager";
						break;
					default:
						_userRole = "Admin";
						break;
				}
			}
		}

		public string Role
		{
			get
			{
				return _userRole;
			}
			set
			{
				_userRole = value;
				switch (value)
				{
					case "Guest":
						_userLevel = 0;
						break;
					case "User":
						_userLevel = 1;
						break;
					case "Manager":
						_userLevel = 2;
						break;
					default:
						_userLevel = 4;
						break;
				}

			}
		}

		[DataMember]
		public string userImage
		{
			get { return _userImage; }
			set { _userImage = value; }
		}

		[DataMember]
		public string usertoken
		{
			get { return _userToken; }
			set { _userToken = value; }
		}

		public override string ToString()
		{
			return
				userID + " " +
				userFirstName + " " +
				userLastName + " " +
				userNickName + " " +
				userBirthDate + " " +
				userGender + " " +
				userEmail + " " +
				userPassword + " " +
				userPicture + " " +
				userLevel + " " +
				usertoken;
		}

		public static UserModel ToObject(DataRow reader)
		{
			DateTime dateValue;

			UserModel userModel = new UserModel();
			userModel.userID = reader[0].ToString();
			userModel.userFirstName = reader[1].ToString();
			userModel.userLastName = reader[2].ToString();
			userModel.userNickName = reader[3].ToString();
			if (DateTime.TryParse(reader[4].ToString(), out dateValue))
			{
				userModel.userBirthDate = DateTime.Parse(reader[4].ToString());
			}
			else
			{
				userModel.userBirthDate = null;
			}
			userModel.userGender = reader[5].ToString();
			userModel.userEmail = reader[6].ToString();
			userModel.userPassword = reader[7].ToString();
			userModel.userPicture = reader[8].ToString();
			if (!userModel.userPicture.Equals(string.Empty) && !userModel.userPicture.Equals(""))
			{
				userModel.userPicture = "/assets/images/users/" + userModel.userPicture;
			}
			userModel.userLevel = int.Parse(reader[9].ToString());

			Debug.WriteLine("userModel:" + userModel.ToString());
			return userModel;
		}

		public static UserModel ToObjectMessage(DataRow reader)
		{
			UserModel userModel = new UserModel();
			userModel.userFirstName = reader[0].ToString();
			userModel.userLastName = reader[1].ToString();
			userModel.userEmail = reader[2].ToString();

			Debug.WriteLine("userModel: " + userModel.ToString());
			return userModel;
		}
	}
}
