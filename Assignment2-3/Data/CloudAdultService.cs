using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public class CloudAdultService : IAdultService
    {
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            HttpClient client = new HttpClient();
            string message = await client.GetStringAsync("https://localhost:5006/Adults");
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message);
            return result;
        }

        public async Task AddAdultAsync(Adult newAdult)
        {
            IList<Adult> adults = await GetAdultsAsync();
            int max = adults.Max(adult => adult.Id);
            newAdult.Id = (++max);
            
            HttpClient client = new HttpClient();
            string adultSerialized = JsonSerializer.Serialize(newAdult);
            StringContent content = new StringContent(
                adultSerialized,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage responseMessage =
                await client.PutAsync("https://localhost:5006/Adults", content);
            Console.WriteLine(responseMessage.StatusCode);  
        }

        public async Task UpdateAdultInfoAsync(Adult newAdult)
        {
            HttpClient client = new HttpClient();
            string adultSerialized = JsonSerializer.Serialize(newAdult);
            StringContent content = new StringContent(
                adultSerialized,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage responseMessage =
                await client.PatchAsync("https://localhost:5006/Adults", content);
            Console.WriteLine(responseMessage.StatusCode);
        }

        public async Task RemoveAdultAsync(Adult adult)
        {
            HttpClient client = new HttpClient();
            var message = await client.DeleteAsync("https://localhost:5006/Adults/"+adult.Id);
            Console.WriteLine(message.StatusCode);
        }
    }
}