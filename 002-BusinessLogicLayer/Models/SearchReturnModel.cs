using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class SearchReturnModel
	{
		private List<FullCarDataModel> _fullCarsData;
		private int _fullCarsDataLenth;
		private int _fullCarsDataPage;

		[DataMember]
		public List<FullCarDataModel> fullCarsData
		{
			get { return _fullCarsData; }
			set { _fullCarsData = value; }
		}

		[DataMember]
		public int fullCarsDataLenth
		{
			get { return _fullCarsDataLenth; }
			set { _fullCarsDataLenth = value; }
		}

		[DataMember]
		public int fullCarsDataPage
		{
			get { return _fullCarsDataPage; }
			set { _fullCarsDataPage = value; }
		}

		public override string ToString()
		{
			return
				fullCarsDataLenth + " " +
				fullCarsDataPage;
		}

		public static SearchReturnModel ToObject(SqlDataReader reader)
		{
			DataTable dt = new DataTable();
			dt.Load(reader);
			int recordCount = dt.Rows.Count;

			SearchReturnModel searchReturnModel = new SearchReturnModel();
			searchReturnModel.fullCarsDataLenth = int.Parse(reader[recordCount - 2].ToString());
			searchReturnModel.fullCarsDataPage = int.Parse(reader[recordCount - 1].ToString());

			Debug.WriteLine("searchReturnModel: " + searchReturnModel.ToString());
			return searchReturnModel;
		}
	}
}
