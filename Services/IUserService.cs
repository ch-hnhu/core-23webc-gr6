// PNSon
using core_23webc_gr6.Models;

namespace core_23webc_gr6.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void SetUsers(List<User> users);
    }
}
// endPNSon