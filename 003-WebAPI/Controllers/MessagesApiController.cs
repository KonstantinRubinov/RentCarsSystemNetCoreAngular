using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class MessagesApiController : ControllerBase
    {
		private IMessagesRepository messagesRepository;
		private IUsersRepository userRepository;

		public MessagesApiController(IMessagesRepository _messagesRepository, IUsersRepository _userRepository)
		{
			messagesRepository = _messagesRepository;
			userRepository = _userRepository;
		}

		[HttpGet("messages")]
		public IActionResult GetAllMessages()
		{
			try
			{
				List<MessageModel> messages = messagesRepository.GetAllMessages();
				return Ok(messages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("messages/{userId}", Name = "GetMessagesByUserId")]
		public IActionResult GetMessagesByUserId(string userId)
		{
			try
			{
				List<MessageModel> messages = messagesRepository.GetMessagesByUserId(userId);
				return Ok(messages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("messages/{messageId}", Name = "GetOneMessageById")]
		public IActionResult GetOneMessageById(int messageId)
		{
			try
			{
				MessageModel message = messagesRepository.GetOneMessageById(messageId);
				return Ok(message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpPost("messages")]
		public IActionResult AddMessage(MessageModel messageModel)
		{
			try
			{
				if (messageModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				string id = User.Identity.Name;
				messageModel.userID = id;
				MessageModel message = messagesRepository.AddMessage(messageModel);
				return StatusCode(StatusCodes.Status201Created, message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpPut("messages/{messageID}")]
		public IActionResult UpdateMessage(int messageID, MessageModel messageModel)
		{
			try
			{
				if (messageModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}
				string id = User.Identity.Name;
				messageModel.userID = id;
				messageModel.messageID = messageID;
				MessageModel message = messagesRepository.UpdateMessage(messageModel);
				return Ok(message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpDelete("messages/{messageId}")]
		public IActionResult DeleteMessage(int messageId)
		{
			try
			{
				int i = messagesRepository.DeleteMessage(messageId);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpDelete("messages/{userId}")]
		public IActionResult DeleteMessageByUser(string userId)
		{
			try
			{
				int i = messagesRepository.DeleteMessageByUser(userId);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}



		[Authorize]
		[HttpGet("messages/userForMessage")]
		public IActionResult GetUserForMessage()
		{
			try
			{
				string id = User.Identity.Name;
				UserModel oneUser = userRepository.GetOneUserForMessageById(id);
				return Ok(oneUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}