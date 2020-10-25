using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string Password);
    }
}