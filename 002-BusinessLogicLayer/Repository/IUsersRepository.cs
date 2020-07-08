using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface IUsersRepository
	{
		UserModel Authenticate(string username, string password);
		List<UserModel> GetAllUsers();
		UserModel GetOneUserById(string id);
		UserModel GetOneUserByName(string name);
		UserModel GetOneUserByLogin(string name, string password);
		UserModel AddUser(UserModel userModel);
		UserModel UpdateUser(UserModel userModel);
		int DeleteUser(string id);
		LoginModel ReturnUserByNamePassword(LoginModel checkUser);
		LoginModel ReturnUserByNameLevel(string username, int userLevel = 0);
		bool IsNameTaken(string name);
		UserModel UploadUserImage(string id, string img);
		UserModel GetOneUserForMessageById(string id);
	}
}
