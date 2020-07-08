using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	public class MongoBranchManager : IBranchRepository
	{
		private readonly IMongoCollection<BranchModel> _branches;

		public MongoBranchManager(CarRentDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_branches = database.GetCollection<BranchModel>(settings.BranchesCollectionName);
		}

		public MongoBranchManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_branches = database.GetCollection<BranchModel>(ConnectionStrings.BranchesCollectionName);
		}

		public List<BranchModel> GetAllBranchesNamesIds()
		{
			return _branches.Find(branch => true).Project(branch => new BranchModel
			{
				branchIDMongo = branch.branchIDMongo,
				branchName = branch.branchName
			}).ToList();
		}

		public List<BranchModel> GetAllBranches()
		{
			return _branches.Find(branch => true).Project(br => new BranchModel
			{
				branchIDMongo = br.branchIDMongo,
				branchID = br.branchID,
				branchName = br.branchName,
				branchAddress = br.branchAddress,
				branchLat = br.branchLat,
				branchLng = br.branchLng
			}).ToList();
		}

		public BranchModel GetOneBranch(int branchID)
		{
			if (branchID < 0)
				throw new ArgumentOutOfRangeException();

			return _branches.Find<BranchModel>(Builders<BranchModel>.Filter.Eq(branch => branch.branchID, branchID)).Project(br => new BranchModel
			{
				branchIDMongo = br.branchIDMongo,
				branchID = br.branchID,
				branchName = br.branchName,
				branchAddress = br.branchAddress,
				branchLat = br.branchLat,
				branchLng = br.branchLng
			}).FirstOrDefault();
		}

		public BranchModel GetOneBranch(string branchIDMongo)
		{
			if (branchIDMongo.Equals(String.Empty))
				throw new ArgumentOutOfRangeException();

			return _branches.Find<BranchModel>(Builders<BranchModel>.Filter.Eq(branch => branch.branchIDMongo, branchIDMongo)).Project(br => new BranchModel
			{
				branchIDMongo = br.branchIDMongo,
				branchID = br.branchID,
				branchName = br.branchName,
				branchAddress = br.branchAddress,
				branchLat = br.branchLat,
				branchLng = br.branchLng
			}).FirstOrDefault();
		}

		public BranchModel GetOneBranchByAddress(string address)
		{
			if (address.Equals(String.Empty))
				throw new ArgumentOutOfRangeException();

			return _branches.Find<BranchModel>(Builders<BranchModel>.Filter.Eq(branch => branch.branchAddress, address)).Project(br => new BranchModel
			{
				branchIDMongo = br.branchIDMongo,
				branchID = br.branchID,
				branchName = br.branchName,
				branchAddress = br.branchAddress,
				branchLat = br.branchLat,
				branchLng = br.branchLng
			}).FirstOrDefault();
		}

		public BranchModel AddBranch(BranchModel branchModel)
		{
			
			if (_branches.Find<BranchModel>(Builders<BranchModel>.Filter.Eq(branch => branch.branchAddress, branchModel.branchAddress)).FirstOrDefault() == null)
			{
				_branches.InsertOne(branchModel);
			}

			BranchModel tmpBranchModel = GetOneBranchByAddress(branchModel.branchAddress);
			return tmpBranchModel;
		}

		public BranchModel UpdateBranch(BranchModel branchModel)
		{
			_branches.ReplaceOne(branch => branch.branchIDMongo.Equals(branchModel.branchIDMongo), branchModel);
			BranchModel tmpBranchModel = GetOneBranch(branchModel.branchIDMongo);
			return tmpBranchModel;
		}

		public int DeleteBranch(int branchID)
		{
			_branches.DeleteOne(branch => branch.branchID.Equals(branchID));
			return 1;
		}

		public int DeleteBranch(string branchIDMongo)
		{
			_branches.DeleteOne(branch => branch.branchIDMongo.Equals(branchIDMongo));
			return 1;
		}
	}
}
