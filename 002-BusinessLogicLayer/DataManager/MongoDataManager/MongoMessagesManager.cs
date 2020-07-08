using MongoDB.Driver;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	public class MongoMessagesManager : IMessagesRepository
	{
		private readonly IMongoCollection<MessageModel> _messages;

		public MongoMessagesManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_messages = database.GetCollection<MessageModel>(settings.MessagesCollectionName);
		}

		public MongoMessagesManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_messages = database.GetCollection<MessageModel>(ConnectionStrings.MessagesCollectionName);
		}


		public List<MessageModel> GetAllMessages()
		{
			return _messages.Find(message => true).Project(m => new MessageModel
			{
				messageID = m.messageID,
				userID = m.userID,
				userFirstName = m.userFirstName,
				userLastName = m.userLastName,
				userEmail = m.userEmail,
				userMessage = m.userMessage,
			}).ToList();
		}


		public List<MessageModel> GetMessagesByUserId(string userID)
		{
			return _messages.Find<MessageModel>(Builders<MessageModel>.Filter.Eq(message => message.userID, userID)).Project(m => new MessageModel
			{
				messageID = m.messageID,
				userID = m.userID,
				userFirstName = m.userFirstName,
				userLastName = m.userLastName,
				userEmail = m.userEmail,
				userMessage = m.userMessage,
			}).ToList();
		}


		public MessageModel GetOneMessageById(int messageID)
		{
			return _messages.Find<MessageModel>(Builders<MessageModel>.Filter.Eq(message => message.messageID, messageID)).Project(m => new MessageModel
			{
				messageID = m.messageID,
				userID = m.userID,
				userFirstName = m.userFirstName,
				userLastName = m.userLastName,
				userEmail = m.userEmail,
				userMessage = m.userMessage,
			}).FirstOrDefault();
		}


		public MessageModel AddMessage(MessageModel messageModel)
		{
			_messages.InsertOne(messageModel);
			MessageModel tmpMessageModel = GetOneMessageById(messageModel.messageID);
			return tmpMessageModel;
		}


		public MessageModel UpdateMessage(MessageModel messageModel)
		{
			_messages.ReplaceOne(message => message.messageID.Equals(messageModel.messageID), messageModel);
			MessageModel tmpMessageModel = GetOneMessageById(messageModel.messageID);
			return tmpMessageModel;
		}


		public int DeleteMessage(int messageId)
		{
			_messages.DeleteOne(message => message.messageID.Equals(messageId));
			return 1;
		}


		public int DeleteMessageByUser(string userId)
		{
			_messages.DeleteMany(message => message.userID.Equals(userId));
			return 1;
		}
	}
}
