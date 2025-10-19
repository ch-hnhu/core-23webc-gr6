// CHNhu - 11/10/2025 - Chuyển từ mysql sang mssql
using Microsoft.Data.SqlClient;

namespace core_23webc_gr6.Helper
{
	public class DatabaseHelper
	{
		private readonly string _connectionString;

		public DatabaseHelper(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("SqlServerConnection") ?? throw new InvalidOperationException("Missing connection string: SqlServerConnection");
		}

		public SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}

		public SqlDataReader ExcuteQuery(string query, SqlConnection conn)
		{
			return new SqlCommand(query, conn).ExecuteReader();
		}

		public SqlDataReader ExcuteQueryWithParameters(string query, SqlParameter[] parameters, SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddRange(parameters);
			return cmd.ExecuteReader();
		}

		public int ExcuteNonQuery(string query, SqlConnection conn)
		{
			return new SqlCommand(query, conn).ExecuteNonQuery();
		}

		public int ExcuteScalar(string query, SqlParameter[] parameters, SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddRange(parameters);
			return Convert.ToInt32(cmd.ExecuteScalar());
		}
	}
}
// endCHNhu