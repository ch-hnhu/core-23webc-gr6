// PNSon
using core_23webc_gr6.Models;
using System.Text.Json;

namespace core_23webc_gr6.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new();
        private readonly string _jsonFilePath;

        public UserService()
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seeds", "db.json");
            LoadUsersFromJson();
        }

        public List<User> GetAllUsers() => _users;

        public void SetUsers(List<User> users) => _users = users;

        //Đã sửa lại hàm này để load đúng dữ liệu users từ db.json 4/10 /2024 khong quan trọng 
        private void LoadUsersFromJson()
        {
            try
            {
                if (File.Exists(_jsonFilePath))
                {
                    var jsonString = File.ReadAllText(_jsonFilePath);

                    // Parse thành dictionary trước
                    var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);

                    if (data != null && data.ContainsKey("users"))
                    {
                        // Lấy mảng users ra và deserialize thành List<User>
                        var usersJson = data["users"].GetRawText();
                        _users = JsonSerializer.Deserialize<List<User>>(usersJson) ?? new List<User>();
                    }
                    else
                    {
                        _users = new List<User>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file db.json: {ex.Message}");
                _users = new List<User>();
            }
        }
    }
}
// endPNSon