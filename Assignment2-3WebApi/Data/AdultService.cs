using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2_3WebApi.Persistance;
using Assignment2_3WebApi.Models;

namespace Assignment2_3WebApi.Data
{
    public class AdultService : IAdultService
    {
        private FileContext fileContext;

        public AdultService()
        {
            fileContext = new FileContext();
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            IList<Adult> adults = fileContext.Adults;
            return adults;
        }

        public async Task AddAdultAsync(Adult newAdult)
        {
            int max = fileContext.Adults.Max(adult => adult.Id);
            newAdult.Id = (++max);
            
            fileContext.Adults.Add(newAdult);
            fileContext.SaveChanges();
        }

        public async Task UpdateAdultInfoAsync(Adult newAdult)
        {
            // update adult info method
            Adult adult = fileContext.Adults.First(a => a.Id == newAdult.Id);
            int indexAt = fileContext.Adults.IndexOf(adult);
            adult.Update(newAdult);
            //update adult list
            fileContext.Adults.RemoveAt(indexAt);
            fileContext.Adults.Insert(indexAt,adult);
            fileContext.SaveChanges();
        }

        public async Task RemoveAdultAsync(int id)
        {
            int position = -1;
            for (int i=0; i<fileContext.Adults.Count;i++)
            {
                if (fileContext.Adults.ElementAt(i).Id == id)
                    position = i;
            }

            if (position != -1)
            {
                fileContext.Adults.RemoveAt(position);
                fileContext.SaveChanges();
            }
        }
    }
}