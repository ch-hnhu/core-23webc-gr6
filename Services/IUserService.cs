//PNSon
using core_group_ex_01.Models;

namespace core_group_ex_01.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void SetUsers(List<User> users);
    }
}
//endPNSon