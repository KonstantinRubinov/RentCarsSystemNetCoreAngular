using System.Data.SqlClient;

namespace RentCarsServerCore
{
	static public class MessageStringsSql
	{
		static private string queryMessagesString = "SELECT * from MESSAGES;";
		static private string queryMessagesByUserIdString = "SELECT * from MESSAGES WHERE userID = @userID;";
		static private string queryMessageString = "SELECT * from MESSAGES WHERE messageID = @messageID;";
		static private string queryMessagePost = "INSERT INTO MESSAGES (userID, userFirstName, userLastName, userEmail, userMessage) VALUES (@userID, @userFirstName, @userLastName, @userEmail, @userMessage); SELECT * FROM MESSAGES where messageID=SCOPE_IDENTITY();";
		static private string queryMessageUpdate = "UPDATE MESSAGES SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userEmail = @userEmail, userMessage = @userMessage where messageID=@messageID; SELECT * from MESSAGES where messageID = @messageID;";
		static private string queryMessageDelete = "DELETE FROM MESSAGES WHERE messageID=@messageID;";
		static private string queryMessagesByUserDelete = "DELETE FROM MESSAGES WHERE userID=@userID;";

		static private string procedureMessagesString = "EXEC GetAllMessages;";
		static private string procedureMessagesByUserIdString = "EXEC GetMessagesByUserId @userID;";
		static private string procedureMessageString = "EXEC GetOneMessageById @messageID;";
		static private string procedureMessagePost = "EXEC AddMessage @userID, @userFirstName, @userLastName, @userEmail, @userMessage;";
		static private string procedureMessageUpdate = "EXEC UpdateMessage @messageID, @userID, @userFirstName, @userLastName, @userEmail, @userMessage;";
		static private string procedureMessageDelete = "EXEC DeleteMessage @messageID;";
		static private string procedureMessagesByUserDelete = "EXEC DeleteMessageByUser @userID;";



		static public SqlCommand GetAllMessages()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryMessagesString);
			else
				return CreateSqlCommand(procedureMessagesString);
		}

		static public SqlCommand GetMessagesByUserId(string userId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userId, queryMessagesByUserIdString);
			else
				return CreateSqlCommand(userId, procedureMessagesByUserIdString);
		}

		static public SqlCommand GetOneMessageById(int messageId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(messageId, queryMessageString);
			else
				return CreateSqlCommand(messageId, procedureMessageString);
		}

		static public SqlCommand AddMessage(MessageModel messageModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(messageModel, queryMessagePost);
			else
				return CreateSqlCommand(messageModel, procedureMessagePost);
		}

		static public SqlCommand UpdateMessage(MessageModel messageModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(messageModel, queryMessageUpdate);
			else
				return CreateSqlCommand(messageModel, procedureMessageUpdate);
		}

		static public SqlCommand DeleteMessage(int messageId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(messageId, queryMessageDelete);
			else
				return CreateSqlCommand(messageId, procedureMessageDelete);
		}

		static public SqlCommand DeleteMessageByUser(string userId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userId, queryMessagesByUserDelete);
			else
				return CreateSqlCommand(userId, procedureMessagesByUserDelete);
		}






		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}

		static private SqlCommand CreateSqlCommand(string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private SqlCommand CreateSqlCommand(int messageID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@messageID", messageID);

			return command;
		}

		static private SqlCommand CreateSqlCommand(MessageModel messageModel, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@messageID", messageModel.messageID);
			command.Parameters.AddWithValue("@userID", messageModel.userID);
			command.Parameters.AddWithValue("@userFirstName", messageModel.userFirstName);
			command.Parameters.AddWithValue("@userLastName", messageModel.userLastName);
			command.Parameters.AddWithValue("@userEmail", messageModel.userEmail);
			command.Parameters.AddWithValue("@userMessage", messageModel.userMessage);

			return command;
		}

	}
}
