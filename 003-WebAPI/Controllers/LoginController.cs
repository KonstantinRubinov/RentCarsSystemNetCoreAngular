using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentCarsServerCore
{
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IUsersRepository usersRepository;

		public LoginController(IUsersRepository _usersRepository)
		{
			usersRepository = _usersRepository;
		}

		[AllowAnonymous]
		[HttpPost("token")]
		public IActionResult Authenticate(LoginModel userParam)
		{
			if (userParam == null)
			{
				return BadRequest("Data is null.");
			}

			var user = usersRepository.Authenticate(userParam.userNickName, userParam.userPassword);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(user);
		}
	}
}