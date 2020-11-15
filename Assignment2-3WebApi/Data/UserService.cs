using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2_3WebApi.DataAccess;
using Assignment2_3WebApi.Models;
using Assignment2_3WebApi.Persistance;

namespace Assignment2_3WebApi.Data
{
    public class UserService : IUserService
    {
        //private FileContext fileContext;
        private Assignment4DBContext dbContext;

        public UserService()
        {
            //fileContext = new FileContext();
            dbContext = new Assignment4DBContext();
        }
        
        public async Task<User> ValidateUser(string userName)
        {
            //   IList<User> users = fileContext.Users;
            IList<User> users = dbContext.Users.ToList(); 
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
            return first;
        }

        public async Task<IList<User>> GetUsers()
        {
            // IList<User> users = fileContext.Users;
            IList<User> users = dbContext.Users.ToList(); 
            return users;
        }
    }
}