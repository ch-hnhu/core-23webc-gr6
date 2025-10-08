//VqNam Tạo file databasehelper để kết nối database 7/10
using MySql.Data.MySqlClient;

namespace core_23webc_gr6.Data.Seeds
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection")
                                ?? throw new InvalidOperationException("Missing connection string: MySqlConnection");
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
//VqNam Tạo file databasehelper để kết nối database 7/10