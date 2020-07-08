using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace RentCarsServerCore
{
	public class MySqlDataBase
	{
		static private MySqlConnection connection = null;
		static private MySqlCommand command = null;
		static private MySqlDataReader reader = null;

		static private MySqlConnection getConnection()
		{
			if (connection == null)
			{
				Debug.WriteLine("Create connection");
				connection = new MySqlConnection(ConnectionStrings.GetMySqlConnection());
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


		static public DataTable GetMultipleQuery(MySqlCommand sqlCommand)
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
					Debug.WriteLine("Create data reader");
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Create data reader Exception: " + ex.Message);
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

		static public int ExecuteNonQuery(MySqlCommand sqlCommand)
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
