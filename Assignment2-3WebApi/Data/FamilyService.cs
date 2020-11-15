using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment2_3WebApi.DataAccess;
using Assignment2_3WebApi.Models;
using Assignment2_3WebApi.Persistance;

namespace Assignment2_3WebApi.Data
{
    public class FamilyService : IFamilyService
    {

        private FileContext fileContext;
        private Assignment4DBContext dbContext;

        public FamilyService()
        {
            fileContext = new FileContext();
            dbContext = new Assignment4DBContext();
        }
        
        public async Task<IList<Family>> GetFamiliesAsync()
        {
            //IList<Family> families = fileContext.Families;
            IList<Family> families = dbContext.Families.ToList();
            // return families;
            IList<Family> familiesToReturn = new List<Family>();
            foreach (var family in families)
            {
                Family fam = family;
                
                fam.Adults = dbContext.Families.Where(f => f.HouseNumber == family.HouseNumber
                                                           && f.StreetName.Equals(family.StreetName))
                    .SelectMany(a => a.Adults).ToList();
                fam.Children = dbContext.Families.Where(f => f.HouseNumber == family.HouseNumber
                                                             && f.StreetName.Equals(family.StreetName))
                    .SelectMany(a => a.Children).ToList();

                foreach (var child in fam.Children)
                {
                    child.ChildInterests = dbContext.Families.Where(f => f.HouseNumber == family.HouseNumber
                                                                         && f.StreetName.Equals(family.StreetName))
                        .SelectMany(a => a.Children).Where(c=>c.Id==child.Id).SelectMany(c=>c.ChildInterests).ToList();
                    child.Pets = dbContext.Families.Where(f => f.HouseNumber == family.HouseNumber
                                                                         && f.StreetName.Equals(family.StreetName))
                        .SelectMany(a => a.Children).Where(c=>c.Id==child.Id).SelectMany(c=>c.Pets).ToList();
                }
                
                fam.Pets = dbContext.Families.Where(f => f.HouseNumber == family.HouseNumber
                                                         && f.StreetName.Equals(family.StreetName))
                    .SelectMany(a => a.Pets).ToList();
                
                familiesToReturn.Add(fam);
            }
            return familiesToReturn;
        }

        public async Task AddFamilyAsync(Family newFamily)
        {
            /*int max = fileContext.Families.Max(family => family.Id);
            newFamily.Id = (++max);
            //check if street+house nr is unique before adding
            if (IsAdressUnique(newFamily))
            {
                fileContext.Families.Add(newFamily);
                fileContext.SaveChanges();
            }*/
            
            int max = dbContext.Families.Max(family => family.Id);
            newFamily.Id = (++max);
            if (IsAdressUnique(newFamily))
            {
                foreach (var adult in newFamily.Adults)
                {
                    dbContext.Remove(adult);
                }
                dbContext.SaveChanges();
                
                dbContext.Families.Add(newFamily);
                dbContext.SaveChanges();
            }
        }

        public async Task<IList<int>> AdultsInFamiliesAsync()
        {
            /*IList<int> adults = new List<int>();
            foreach (var family in fileContext.Families)
            {
                foreach (var adult in family.Adults)
                {
                    adults.Add(adult.Id);
                }
            }
            return adults; */

            IList<Adult> adults = dbContext.Families.SelectMany(a=>a.Adults).ToList();
            IList<int> ids = new List<int>();
            foreach (var adult in adults)
            {
                ids.Add(adult.Id);
            }
            return ids;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            /*
            Family familyToRemove = null;
            Adult adult = null;
            foreach (var ad in fileContext.Adults)
            {
                if (ad.Id == adultId)
                {
                    adult = ad;
                }
            }

            if (adult != null)
            {
                foreach (var family in fileContext.Families)
                {
                    if (family.Adults.Contains(adult))
                    {
                        family.Adults.Remove(adult);
                        if (family.Adults.Count == 0)
                            familyToRemove = family;
                        fileContext.SaveChanges();
                    }
                }

                if (familyToRemove != null)
                {
                    fileContext.Families.Remove(familyToRemove);
                    fileContext.SaveChanges();
                }
            }
            */
            
            //no need for this anymore since the database should update automatically 
        }

        private bool IsAdressUnique(Family newFamily)
        {
            IList<Family> families = fileContext.Families;
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