//VqNam Tạo file databasehelper để kết nối database 7/10
// CHNhu - 11/10/2025 - Chuyển từ mysql sang mssql
using Microsoft.Data.SqlClient;

namespace core_23webc_gr6.Data
{
	public class DatabaseHelper
	{
		private readonly string _connectionString;

		public DatabaseHelper(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("SqlServerConnection")
								?? throw new InvalidOperationException("Missing connection string: SqlServerConnection");
		}

		public SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
// endCHNhu
//VqNam Tạo file databasehelper để kết nối database 7/10