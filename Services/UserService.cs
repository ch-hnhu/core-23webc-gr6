//PNSon
using core_group_ex_01.Models;

namespace core_group_ex_01.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new();

        public List<User> GetAllUsers() => _users;

        public void SetUsers(List<User> users) => _users = users;
    }
}
//endPNSon