using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class UsersApiController : ControllerBase
    {
		private IUsersRepository usersRepository;
		private readonly IHostingEnvironment environment;

		public UsersApiController(IUsersRepository _usersRepository, IHostingEnvironment _environment)
		{
			usersRepository = _usersRepository;
			environment = _environment;
		}

		[Authorize]
		[HttpGet("users")]
		public IActionResult GetAllUsers()
		{
			try
			{
				List<UserModel> allUsers = usersRepository.GetAllUsers();
				return Ok(allUsers);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpGet("users/{id}", Name = "GetOneUser")]
		public IActionResult GetOneUser(string id)
		{
			try
			{
				UserModel oneUser = usersRepository.GetOneUserById(id);
				return Ok(oneUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPost("users/check")]
		public IActionResult ReturnUserByNamePassword(LoginModel loginModel)
		{
			try
			{
				if (loginModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				LoginModel checkUser = usersRepository.ReturnUserByNamePassword(loginModel);
				if (checkUser == null)
				{
					Errors errors = ErrorsHelper.GetErrors("Wrong username or password");
					return StatusCode(StatusCodes.Status401Unauthorized, errors);
				}
				return StatusCode(StatusCodes.Status201Created, checkUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPost("users")]
		public IActionResult AddUser(UserModel userModel)
		{
			try
			{
				if (userModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				byte[] bytes = Convert.FromBase64String(userModel.userImage);
				string[] extensions = userModel.userPicture.Split('.');
				string extension = extensions[extensions.Length - 1];
				string fileName = Guid.NewGuid().ToString();
				string filePath = environment.WebRootPath + "/assets/images/users/" + fileName + "." + extension;
				using (FileStream binaryFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
				{
					binaryFileStream.Write(bytes, 0, bytes.Length);
					userModel.userPicture = fileName + "." + extension;
				}
				userModel.userImage = string.Empty;
				UserModel addedUser = usersRepository.AddUser(userModel);
				return StatusCode(StatusCodes.Status201Created, addedUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpPut("users/{id}")]
		public IActionResult UpdateUser(string id, UserModel userModel)
		{
			try
			{
				if (userModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				userModel.userID = id;
				UserModel updatedUser = usersRepository.UpdateUser(userModel);
				return Ok(updatedUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpDelete("users/{id}")]
		public IActionResult DeleteUser(string id)
		{
			try
			{
				int i = usersRepository.DeleteUser(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}




		//[HttpPost("products/file/{id}")]
		//public IActionResult UploadUserImage(string id)
		//{
		//	try
		//	{
		//		byte[] bytes = Convert.FromBase64String(userModel.userImage);
		//		UserModel updloadedUser;
		//		string fileName = Guid.NewGuid().ToString();
		//		string filePath = environment.WebRootPath + "/assets/images/users/" + fileName + "." + extension;
		//		using (FileStream binaryFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
		//		{
		//			binaryFileStream.Write(bytes, 0, bytes.Length);
		//			updloadedUser = usersRepository.UploadUserImage(id, fileName);
		//			updloadedUser.userPicture = "assets/images/users/" + fileName;
		//		}
		//		if (updloadedUser != null)
		//		{
		//			return Ok(updloadedUser);
		//		}
		//		return StatusCode(StatusCodes.Status417ExpectationFailed);

		//	}
		//	catch (Exception ex)
		//	{
		//		Errors errors = ErrorsHelper.GetErrors(ex);
		//		return StatusCode(StatusCodes.Status500InternalServerError, errors);
		//	}
		//}
	}
}