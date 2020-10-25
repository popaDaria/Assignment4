using System;
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
            string message = await client.GetStringAsync("http://dnp.metamate.me/Adults");
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message);
            return result;
        }

        public async Task AddAdultAsync(Adult newAdult)
        {
            newAdult.Id = GetAdultsAsync().Result.ElementAt(GetAdultsAsync().Result.Count - 1).Id + 1;
            HttpClient client = new HttpClient();
            string adultSerialized = JsonSerializer.Serialize(newAdult);
            StringContent content = new StringContent(
                adultSerialized,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage responseMessage =
                await client.PostAsync("http://dnp.metamate.me/Adults", content);
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
                await client.PatchAsync("http://dnp.metamate.me/Adults/"+newAdult.Id, content);
            Console.WriteLine(responseMessage.StatusCode);
        }

        public async Task RemoveAdultAsync(Adult adult)
        {
            HttpClient client = new HttpClient();
            var message = await client.DeleteAsync("http://dnp.metamate.me/Adults/"+adult.Id);
            Console.WriteLine(message.StatusCode);
        }
    }
}