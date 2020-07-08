using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class MessageModel
	{
		private int _messageID;
		private string _userID;
		private string _userFirstName;
		private string _userLastName;
		private string _userEmail;
		private string _userMessage;

		[DataMember]
		public int messageID
		{
			get { return _messageID; }
			set { _messageID = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user id.")]
		[PossibleID]
		public string userID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user first name.")]
		[StringLength(40, ErrorMessage = "User first name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "User first name mast be minimum 2 chars.")]
		[RegularExpression("^[A-Z].*$", ErrorMessage = "User first name must start with a capital letter.")]
		public string userFirstName
		{
			get { return _userFirstName; }
			set { _userFirstName = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user last name.")]
		[StringLength(40, ErrorMessage = "User last name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "User first last mast be minimum 2 chars.")]
		[RegularExpression("^[A-Z].*$", ErrorMessage = "User last name must start with a capital letter.")]
		public string userLastName
		{
			get { return _userLastName; }
			set { _userLastName = value; }
		}

		const string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
		[Required(ErrorMessage = "Missing user email.")]
		[RegularExpression(pattern, ErrorMessage = "User mail wrong.")]
		[DataMember]
		public string userEmail
		{
			get { return _userEmail; }
			set { _userEmail = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing user message.")]
		public string userMessage
		{
			get { return _userMessage; }
			set { _userMessage = value; }
		}

		public override string ToString()
		{
			return
				messageID + " " +
				userID + " " +
				userFirstName + " " +
				userLastName + " " +
				userEmail + " " +
				userMessage;
		}
		public static MessageModel ToObject(DataRow reader)
		{
			MessageModel messageModel = new MessageModel();
			messageModel.messageID = int.Parse(reader[0].ToString());
			messageModel.userID = reader[1].ToString();
			messageModel.userFirstName = reader[2].ToString();
			messageModel.userLastName = reader[3].ToString();
			messageModel.userEmail = reader[4].ToString();
			messageModel.userMessage = reader[5].ToString();

			Debug.WriteLine("messageModel: " + messageModel.ToString());
			return messageModel;
		}
	}
}
