using Microsoft.Data.SqlClient;

namespace core_23webc_gr6.Helper
{
	public class DatabaseHelper
	{
		private string _connectionString;

		public DatabaseHelper(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("SqlServerConnection") ?? throw new InvalidOperationException("Connection string 'SqlServerConnection' not found.");
		}

		public SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}

		public SqlDataReader ExcuteQuery(string query, SqlConnection conn)
		{
			try
			{
				return new SqlCommand(query, conn).ExecuteReader();
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi khi thực thi truy vấn: " + ex.Message);
			}
		}

		public SqlDataReader ExcuteQueryWithParams(string query, SqlConnection conn, SqlParameter[] parameters)
		{
			try
			{
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddRange(parameters);
				return cmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi khi thực thi truy vấn với tham số: " + ex.Message);
			}
		}
	}
}