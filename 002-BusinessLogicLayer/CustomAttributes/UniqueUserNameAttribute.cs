using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RentCarsServerCore
{
	public class UniqueUserNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || value.ToString() == "")
				return ValidationResult.Success;

			string name = value.ToString();

			IUsersRepository usersRepository = null;
			//InterfaceUsersLogic logic = new EntityUsersLogic();



			if (GlobalVariable.logicType == 1)
			{
				usersRepository = new SqlUsersManager();
			}
			else if (GlobalVariable.logicType == 2)
			{
				usersRepository = new MySqlUsersManager();
			}
			else if (GlobalVariable.logicType == 3)
			{
				usersRepository = new MongoUsersManager();
			}

			if (usersRepository.IsNameTaken(name))
			{
				Debug.WriteLine("User name " + name + " already taken!");
				return new ValidationResult("User name " + name + " already taken!");
			}

			Debug.WriteLine("User name " + name + " is ok!");
			return ValidationResult.Success;
		}
	}
}
