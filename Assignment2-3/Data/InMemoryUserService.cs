using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2_3.Models;

namespace Assignment2_3.Data
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
        public User ValidateUser(string userName, string password)
        {
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
            if (first == null) {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password)) {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}