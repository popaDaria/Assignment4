using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public class CloudUserService : IUserService
    {
        public async Task<User> ValidateUser(string userName, string Password)
        {
            HttpClient client = new HttpClient();
            string message = await client.GetStringAsync("https://localhost:5006/Users?userName="+userName);
            Console.WriteLine(message);
            User result = JsonSerializer.Deserialize<User>(message);
            Console.WriteLine(result.UserName);
            
            if (result == null) {
                throw new Exception("User not found");
            }
            if (!result.Password.Equals(Password)) {
                throw new Exception("Incorrect password");
            }
            return result;
        }
    }
}