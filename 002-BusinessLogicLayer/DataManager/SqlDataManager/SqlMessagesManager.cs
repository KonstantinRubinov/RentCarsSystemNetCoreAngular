using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RentCarsServerCore
{
	public class SqlMessagesManager : SqlDataBase, IMessagesRepository
	{
		public List<MessageModel> GetAllMessages()
		{
			DataTable dt = new DataTable();
			List<MessageModel> arrMessage = new List<MessageModel>();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsSql.GetAllMessages());
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsSql.GetMessagesByUserId(userId));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsSql.GetOneMessageById(messageId));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsSql.AddMessage(messageModel));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MessageStringsSql.UpdateMessage(messageModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(MessageStringsSql.DeleteMessage(messageId));
			}
			return i;
		}

		public int DeleteMessageByUser(string userId)
		{
			int i = 0;
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(MessageStringsSql.DeleteMessageByUser(userId));
			}
			return i;
		}
	}
}
