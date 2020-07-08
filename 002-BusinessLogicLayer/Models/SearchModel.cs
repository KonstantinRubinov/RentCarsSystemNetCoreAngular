using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RentCarsServerCore
{
	[DataContract]
	public class SearchModel
	{
		private DateTime? _fromDate;
		private DateTime? _toDate;
		private string _company = "";
		private string _carType = "";
		private string _gear = "";
		private int _year = 0;
		private string _freeSearch = "";

		[DataMember]
		public DateTime? fromDate
		{
			get { return _fromDate; }
			set { _fromDate = value; }
		}

		[DataMember]
		public DateTime? toDate
		{
			get { return _toDate; }
			set { _toDate = value; }
		}

		[DataMember]
		public string company
		{
			get { return _company; }
			set { _company = value; }
		}

		[DataMember]
		public string carType
		{
			get { return _carType; }
			set { _carType = value; }
		}

		[DataMember]
		public string gear
		{
			get { return _gear; }
			set { _gear = value; }
		}

		[DataMember]
		public int year
		{
			get { return _year; }
			set { _year = value; }
		}

		[DataMember]
		public string freeSearch
		{
			get { return _freeSearch; }
			set { _freeSearch = value; }
		}

		public override string ToString()
		{
			return
				fromDate + " " +
				toDate + " " +
				company + " " +
				carType + " " +
				gear + " " +
				year + " " +
				freeSearch;
		}

		public static SearchModel ToObject(SqlDataReader reader)
		{
			DateTime dateValue;
			SearchModel searchModel = new SearchModel();
			if (DateTime.TryParse(reader[0].ToString(), out dateValue))
			{
				searchModel.fromDate = DateTime.Parse(reader[0].ToString());
			}
			else
			{
				searchModel.fromDate = null;
			}

			if (DateTime.TryParse(reader[1].ToString(), out dateValue))
			{
				searchModel.toDate = DateTime.Parse(reader[1].ToString());
			}
			else
			{
				searchModel.toDate = null;
			}
			searchModel.company = reader[2].ToString();
			searchModel.carType = reader[3].ToString();
			searchModel.gear = reader[4].ToString();
			searchModel.year = int.Parse(reader[5].ToString());
			searchModel.freeSearch = reader[6].ToString();

			Debug.WriteLine("searchModel: " + searchModel.ToString());
			return searchModel;
		}
	}
}
