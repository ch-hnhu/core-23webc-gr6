//PNSon
using core_group_ex_01.Models;
using System.Text.Json;

namespace core_group_ex_01.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new();
        private readonly string _jsonFilePath;

        public UserService()
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Users.json");
            LoadUsersFromJson();
        }

        public List<User> GetAllUsers() => _users;

        public void SetUsers(List<User> users) => _users = users;

        private void LoadUsersFromJson()
        {
            try
            {
                if (File.Exists(_jsonFilePath))
                {
                    var jsonString = File.ReadAllText(_jsonFilePath);
                    _users = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
                }
            }
            catch (Exception ex)
            {
                // Log error nếu có lỗi khi đọc file
                Console.WriteLine($"Lỗi khi đọc file Users.json: {ex.Message}");
                _users = new List<User>();
            }
        }
    }
}
//endPNSon