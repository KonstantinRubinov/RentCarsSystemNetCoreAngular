using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RentCarsServerCore
{
	public class SqlDataBase
	{
		static private SqlConnection connection = null;
		static private SqlCommand command = null;
		static private SqlDataReader reader = null;

		static private SqlConnection getConnection()
		{
			if (connection == null)
			{
				Debug.WriteLine("Create connection");
				connection = new SqlConnection(ConnectionStrings.GetSqlConnection());
			}
			Debug.WriteLine("Return connection");
			return connection;
		}

		static public void Connect()
		{
			if (connection.State != ConnectionState.Open)
			{
				Debug.WriteLine("Open connection");
				connection.Open();
			}
		}

		static public void Disconnect()
		{
			Debug.WriteLine("Close connection");
			connection.Close();
		}


		static public DataTable GetMultipleQuery(SqlCommand sqlCommand)
		{
			DataTable datatable = new DataTable();
			getConnection();
			lock (connection)
			{
				command = sqlCommand;
				Connect();
				command.Connection = connection;
				try
				{
					reader = command.ExecuteReader();
					datatable.Load(reader);
					Debug.WriteLine("Create deta reader");
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Create deta reader Exception: " + ex.Message);
					if (reader != null)
					{
						reader.Close();
					}
				}
				finally
				{
					Disconnect();
				}
			}
			return datatable;
		}

		static public int ExecuteNonQuery(SqlCommand sqlCommand)
		{
			int i = 0;
			getConnection();
			lock (connection)
			{
				command = sqlCommand;
				Connect();
				command.Connection = connection;
				try
				{
					i = command.ExecuteNonQuery();
					Debug.WriteLine("Create NonQuery: " + i);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Create NonQuery :" + ex.Message);
				}
				finally
				{
					Disconnect();
				}
			}
			return i;
		}
	}
}
