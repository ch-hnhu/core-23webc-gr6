using System;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;

// PNSon thêm 11/10/2025
// CHNhu - 12/10/2025 - Sửa lại tên model
namespace core_23webc_gr6.Models
{
	public class Users
	{
		public int userId { get; set; }                 // Khóa chính
		public string username { get; set; } = "";      // Tên đăng nhập
		public string email { get; set; } = "";         // Địa chỉ email (duy nhất)
		public string password { get; set; } = "";      // Mật khẩu
		public string role { get; set; } = "User";      // Vai trò (User/Admin)
		public int status { get; set; } = 1;        // Trạng thái hoạt động
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật

		// CHNhu - Hàm lấy user theo username và password
		public static Users? GetUser(string username, string password, DatabaseHelper dbHelper)
		{
			try
			{
				using var conn = dbHelper.GetConnection();
				conn.Open();

				string query = "SELECT * FROM Users WHERE username = @username AND password = @password AND status = 1";
				using var cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@password", password);

				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					return new Users
					{
						userId = Convert.ToInt32(reader["userId"]),
						username = reader["username"].ToString() ?? "",
						email = reader["email"].ToString() ?? "",
						password = reader["password"].ToString() ?? "",
						role = reader["role"].ToString() ?? "User",
						status = Convert.ToInt32(reader["status"]),
						createdAt = Convert.ToDateTime(reader["createdAt"]),
						updatedAt = Convert.ToDateTime(reader["updatedAt"])
					};
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
	}
}
// endPNSon
