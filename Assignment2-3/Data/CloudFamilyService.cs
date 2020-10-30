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
    public class CloudFamilyService : IFamilyService
    {
        public async Task<IList<Family>> GetFamiliesAsync()
        {
            HttpClient client = new HttpClient();
            string message = await client.GetStringAsync("https://localhost:5006/Families");
            List<Family> result = JsonSerializer.Deserialize<List<Family>>(message);
            return result;
        }

        public async Task AddFamilyAsync(Family newFamily)
        {
            IList<Family> families = await GetFamiliesAsync();
            int max = families.Max(family => family.Id);
            newFamily.Id = (++max);

            /*//USING PREMADE ADULTS DOES NOT WORK FOR SOME REASON :/
            foreach (var adult in newFamily.Adults)
            {
                adult.Id+=7000;
            }*/
            bool isUnique = await IsAdressUnique(newFamily);
            if (isUnique)
            {
                HttpClient client = new HttpClient();
                string familySerialize = JsonSerializer.Serialize(newFamily);
                Console.WriteLine(familySerialize);
                
                StringContent content = new StringContent(
                    familySerialize,
                    Encoding.UTF8,
                    "application/json"
                );
                HttpResponseMessage responseMessage =
                    await client.PutAsync("https://localhost:5006/Families", content);
                Console.WriteLine(responseMessage.StatusCode);
            }
        }

        public async Task<IList<int>> AdultsInFamiliesAsync()
        {
            IList<int> adults = new List<int>();
            IList<Family> families = await GetFamiliesAsync();
            foreach (var family in families)
            {
                foreach (var adult in family.Adults)
                {
                    adults.Add(adult.Id);
                    //REMOVE THIS WHEN MAKING YOUR OWN LIST
                    /*if(adult.Id>7000)
                        adults.Add(adult.Id - 7000);*/
                }
            }
            return adults;
        }
    

        public async Task RemoveAdultAsync(Adult adult)
        {
            
            Family familyToRemove = null;
            IList<Family> families = await GetFamiliesAsync();
            foreach (var family in families)
            {
                if (family.Adults.Contains(adult))
                {
                    familyToRemove = family;
                }
            }

            if (familyToRemove != null)
            {
                HttpClient client = new HttpClient();
                var message = await client.DeleteAsync("https://localhost:5006/Families?adultId="+adult.Id);
                
                Console.WriteLine(message.StatusCode);
                
                familyToRemove.Adults.Remove(adult);
                if (familyToRemove.Adults.Count != 0)
                {
                    await AddFamilyAsync(familyToRemove);
                }
            }
            
        }
        
        private async Task<bool> IsAdressUnique(Family newFamily)
        {
            IList<Family> families = await GetFamiliesAsync();
            bool unique = true;
            foreach (Family fam in families)
            {
                if (fam.HouseNumber == newFamily.HouseNumber && fam.StreetName.Equals(newFamily.StreetName))
                    unique = false;
            }
            return unique;
        }
    }
}