using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3WebApi.Models;

namespace Assignment2_3WebApi.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName);
        Task<IList<User>> GetUsers();
    }
}