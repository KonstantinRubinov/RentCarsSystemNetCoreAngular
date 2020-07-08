using System.Collections.Generic;

namespace RentCarsServerCore
{
	public interface IMessagesRepository
	{
		List<MessageModel> GetAllMessages();
		List<MessageModel> GetMessagesByUserId(string userId);
		MessageModel GetOneMessageById(int messageId);
		MessageModel AddMessage(MessageModel messageModel);
		MessageModel UpdateMessage(MessageModel messageModel);
		int DeleteMessage(int messageId);
		int DeleteMessageByUser(string userId);
	}
}
