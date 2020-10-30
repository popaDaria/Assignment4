using System.Threading.Tasks;
using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName, string Password);
    }
}