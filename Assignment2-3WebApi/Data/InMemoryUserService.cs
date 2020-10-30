using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2_3WebApi.Models;

namespace Assignment2_3WebApi.Data
{
    public class InMemoryUserService : IUserService
    {
        private List<User> users;

        public InMemoryUserService()
        {
            users = new List<User>()
            {
                new User
                {
                    Password = "123456",
                    Role = "Admin",
                    UserName = "Troels"
                },
                new User
                {
                    Password = "654321",
                    Role = "User",
                    UserName = "Jakob"
                },
                new User {
                    Password = "123456",
                    Role = "User",
                    UserName = "Kasper"
                },
                new User {
                    Password = "111",
                    Role = "Admin",
                    UserName = "qqq"
                }
            }.ToList();

        }
        public async Task<User> ValidateUser(string userName)
        {
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
            /*if (first == null) {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password)) {
                throw new Exception("Incorrect password");
            }*/
            return first;
        }
    }
}