using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace RentCarsServerCore
{
	public class MySqlMessagesManager : MySqlDataBase, IMessagesRepository
	{
		public List<MessageModel> GetAllMessages()
		{
			DataTable dt = new DataTable();
			List<MessageModel> arrMessage = new List<MessageModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsMySql.GetAllMessages());
			}
			foreach (DataRow ms in dt.Rows)
			{
				arrMessage.Add(MessageModel.ToObject(ms));
			}

			return arrMessage;
		}

		public List<MessageModel> GetMessagesByUserId(string userId)
		{
			DataTable dt = new DataTable();
			List<MessageModel> arrMessage = new List<MessageModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsMySql.GetMessagesByUserId(userId));
			}
			foreach (DataRow ms in dt.Rows)
			{
				arrMessage.Add(MessageModel.ToObject(ms));
			}

			return arrMessage;
		}

		public MessageModel GetOneMessageById(int messageId)
		{
			DataTable dt = new DataTable();
			MessageModel message = new MessageModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsMySql.GetOneMessageById(messageId));
			}
			foreach (DataRow ms in dt.Rows)
			{
				message = MessageModel.ToObject(ms);
			}

			return message;
		}

		public MessageModel AddMessage(MessageModel messageModel)
		{
			DataTable dt = new DataTable();
			MessageModel message = new MessageModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsMySql.AddMessage(messageModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				message = MessageModel.ToObject(ms);
			}

			return message;
		}

		public MessageModel UpdateMessage(MessageModel messageModel)
		{
			DataTable dt = new DataTable();
			MessageModel message = new MessageModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsMySql.UpdateMessage(messageModel));
			}

			foreach (DataRow ms in dt.Rows)
			{
				message = MessageModel.ToObject(ms);
			}

			return message;
		}

		public int DeleteMessage(int messageId)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(MessageStringsMySql.DeleteMessage(messageId));
			}
			return i;
		}

		public int DeleteMessageByUser(string userId)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(MessageStringsMySql.DeleteMessageByUser(userId));
			}
			return i;
		}
	}
}
