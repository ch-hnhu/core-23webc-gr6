using System;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using System.Text.Json;

// PNSon thêm 11/10/2025
// CHNhu - 12/10/2025 - Sửa lại tên model
namespace core_23webc_gr6.Models
{
	public class User
	{
		public int userId { get; set; }                 // Khóa chính
		public string username { get; set; } = "";      // Tên đăng nhập
		public string email { get; set; } = "";         // Địa chỉ email (duy nhất)
		public string password { get; set; } = "";      // Mật khẩu
		public string role { get; set; } = "User";      // Vai trò (User/Admin)
		public int status { get; set; } = 1;        // Trạng thái hoạt động
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật

		// CHNhu - 20/10/2025 - Hàm lấy user theo username và password
		#region GetUser
		public static string? GetUser(string username, string password, DatabaseHelper dbHelper)
		{
			try
			{
				User? user = null;
				string query = "SELECT * FROM Users WHERE username = @username AND password = @password AND status = 1";
				SqlParameter[] parameters =
				[
					new SqlParameter("@username", username),
					new SqlParameter("@password", password)
				];

				SqlConnection conn = dbHelper.GetConnection();
				conn.Open();
				SqlDataReader dr = dbHelper.ExcuteQueryWithParameters(query, parameters, conn);
				while (dr.Read())
				{
					user = new User
					{
						userId = Convert.ToInt32(dr["userId"]),
						username = dr["username"].ToString() ?? "",
						email = dr["email"].ToString() ?? "",
						password = dr["password"].ToString() ?? "",
						role = dr["role"].ToString() ?? "User",
						status = Convert.ToInt32(dr["status"]),
						createdAt = Convert.ToDateTime(dr["createdAt"]),
						updatedAt = Convert.ToDateTime(dr["updatedAt"])
					};

				}
				dr.Close();
				conn.Close();
				return user != null ? JsonSerializer.Serialize(user) : null;
			}
			catch (Exception)
			{
				throw new Exception("Lỗi truy vấn user với username và password.");
			}
		}
		#endregion
		// CHNhu - 19/10/2025 - Hàm lấy tất cả user
		#region GetAllUsers
		public static string? GetAllUsers(DatabaseHelper dbHelper)
		{
			try
			{
				string query = "SELECT * FROM Users WHERE status = 1";
				List<User> users = new List<User>();
				SqlConnection conn = dbHelper.GetConnection();
				conn.Open();
				SqlDataReader dr = dbHelper.ExcuteQuery(query, conn);
				while (dr.Read())
				{
					users.Add(new User
					{
						userId = Convert.ToInt32(dr["userId"]),
						username = dr["username"].ToString() ?? "",
						email = dr["email"].ToString() ?? "",
						password = dr["password"].ToString() ?? "",
						role = dr["role"].ToString() ?? "User",
						status = Convert.ToInt32(dr["status"]),
						createdAt = Convert.ToDateTime(dr["createdAt"]),
						updatedAt = Convert.ToDateTime(dr["updatedAt"])
					});
				}
				dr.Close();
				conn.Close();

				return users != null ? JsonSerializer.Serialize(users) : null;
			}
			catch (Exception)
			{
				throw new Exception("Lỗi lấy danh sách user.");
			}
		}
		#endregion
	}
}
// endPNSon
