using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class BranchModel
	{
		private string _branchIDMongo;

		private int _branchID;
		private string _branchName;
		private string _branchAddress;
		private double _branchLat;
		private double _branchLng;

		public BranchModel(string tmpBranchIDMongo, string tmpBranchName, string tmpBranchAddress, double tmpBranchLat, double tmpBranchLng)
		{
			branchIDMongo = tmpBranchIDMongo;
			branchName = tmpBranchName;
			branchAddress = tmpBranchAddress;
			branchLat = tmpBranchLat;
			branchLng = tmpBranchLng;
		}

		public BranchModel(int tmpBranchID, string tmpBranchName, string tmpBranchAddress, double tmpBranchLat, double tmpBranchLng)
		{
			branchID = tmpBranchID;
			branchName = tmpBranchName;
			branchAddress = tmpBranchAddress;
			branchLat = tmpBranchLat;
			branchLng = tmpBranchLng;
		}

		public BranchModel(string tmpBranchName, string tmpBranchAddress, double tmpBranchLat, double tmpBranchLng)
		{
			branchName = tmpBranchName;
			branchAddress = tmpBranchAddress;
			branchLat = tmpBranchLat;
			branchLng = tmpBranchLng;
		}

		public BranchModel()
		{

		}

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string branchIDMongo
		{
			get { return _branchIDMongo; }
			set { _branchIDMongo = value; }
		}

		[DataMember]
		public int branchID
		{
			get { return _branchID; }
			set { _branchID = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing branch name.")]
		[StringLength(40, ErrorMessage = "Branch name can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "Branch name mast be minimum 2 chars.")]
		public string branchName
		{
			get { return _branchName; }
			set { _branchName = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing Branch Address.")]
		[StringLength(40, ErrorMessage = "Branch Address can't exceeds 40 chars.")]
		[MinLength(2, ErrorMessage = "Branch Address mast be minimum 2 chars.")]
		public string branchAddress
		{
			get { return _branchAddress; }
			set { _branchAddress = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing Branch Latitude.")]
		public double branchLat
		{
			get { return _branchLat; }
			set { _branchLat = value; }
		}

		[DataMember]
		[Required(ErrorMessage = "Missing Branch Longitude.")]
		public double branchLng
		{
			get { return _branchLng; }
			set { _branchLng = value; }
		}

		public override string ToString()
		{
			return
				branchAddress + " " +
				branchLat + " " +
				branchLat + " " +
				branchLng + " " +
				branchName + " " +
				branchID + " " +
				branchIDMongo;
		}

		public static BranchModel ToObject(DataRow reader)
		{
			BranchModel branchModel = new BranchModel();
			branchModel.branchAddress = reader[0].ToString();
			branchModel.branchLat = double.Parse(reader[1].ToString());
			branchModel.branchLng = double.Parse(reader[2].ToString());
			branchModel.branchName = reader[3].ToString();
			branchModel.branchID = int.Parse(reader[4].ToString());
			Debug.WriteLine("branchModel: " + branchModel.ToString());
			return branchModel;
		}
	}
}
