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
    public class CloudFamilyService : IFamilyService
    {
        public async Task<IList<Family>> GetFamiliesAsync()
        {
            HttpClient client = new HttpClient();
            string message = await client.GetStringAsync("http://dnp.metamate.me/Families");
            List<Family> result = JsonSerializer.Deserialize<List<Family>>(message);
            return result;
        }

        public async Task AddFamilyAsync(Family newFamily)
        {
            newFamily.Id = GetFamiliesAsync().Result.ElementAt(GetFamiliesAsync().Result.Count - 1).Id + 1;
            if (IsAdressUnique(newFamily))
            {
                HttpClient client = new HttpClient();
                string familySerialize = JsonSerializer.Serialize(newFamily);
                StringContent content = new StringContent(
                    familySerialize,
                    Encoding.UTF8,
                    "application/json"
                );
                HttpResponseMessage responseMessage =
                    await client.PostAsync("http://dnp.metamate.me/Families", content);
                Console.WriteLine(responseMessage.StatusCode);
            }
        }

        public async Task<IList<int>> AdultsInFamiliesAsync()
        {
            IList<int> adults = new List<int>();
            foreach (var family in GetFamiliesAsync().Result)
            {
                foreach (var adult in family.Adults)
                {
                    adults.Add(adult.Id);
                }
            }

            return adults;
        }
    

        public async Task RemoveAdultAsync(Adult adult)
        {
            Family familyToRemove = null;
            foreach (var family in GetFamiliesAsync().Result)
            {
                if (family.Adults.Contains(adult))
                {
                    familyToRemove = family;
                }
            }

            if (familyToRemove != null)
            {
                HttpClient client = new HttpClient();
                var message = await client.DeleteAsync("http://dnp.metamate.me/Families?streetname="+familyToRemove.StreetName+"&housenumber="+familyToRemove.HouseNumber);
                Console.WriteLine(message.StatusCode);
                
                familyToRemove.Adults.Remove(adult);
                if (familyToRemove.Adults.Count != 0)
                {
                    AddFamilyAsync(familyToRemove);
                }
            }
        }
        
        private bool IsAdressUnique(Family newFamily)
        {
            IList<Family> families = GetFamiliesAsync().Result;
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