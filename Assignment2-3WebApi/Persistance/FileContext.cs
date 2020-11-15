using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment2_3WebApi.DataAccess;
using Assignment2_3WebApi.Models;

namespace Assignment2_3WebApi.Persistance {
    public class FileContext
    {

        private static Assignment4DBContext dbContext = new Assignment4DBContext();
        public IList<Family> Families { get; private set; }
        public IList<Adult> Adults { get; private set; }
        public IList<User> Users { get; private set; }

        private readonly string familiesFile = "families.json";
        private readonly string adultsFile = "adults.json";
        private readonly string usersFile = "users.json";

        public FileContext() {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();

            /*if (!dbContext.Users.Any())
            {
                foreach (var user in Users)
                {
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
            }
            
            for (int j=20;j<Families.Count;j++)
            {
                Family fam = Families.ElementAt(j);
                if (dbContext.Families.Any(a =>
                    a.HouseNumber == fam.HouseNumber && a.StreetName.Equals(fam.StreetName)) == false)
                {
            
                    if (fam.Children.Count != 0)
                    {
                        foreach (var child in fam.Children)
                        {
                            foreach (var interest in child.ChildInterests)
                            {
                                Interest intr = new Interest();
                                intr.Type = interest.InterestId;
                                if (!dbContext.Interest.Any(i => i.Type.Equals(intr.Type)))
                                    dbContext.Interest.Add(intr);
                            }
            
                            dbContext.SaveChanges();
                        }
                    }
                    
                    dbContext.Add(fam);
                    dbContext.SaveChanges();
                }
            }
            */
        }

        private IList<T> ReadData<T>(string s) {
            using (var jsonReader = File.OpenText(s)) {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges() {
            // storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions {
                WriteIndented = true
            });

            using (StreamWriter outputFile = new StreamWriter(familiesFile, false)) {
                outputFile.Write(jsonFamilies);
            }

            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions {
                WriteIndented = true
            });

            using (StreamWriter outputFile = new StreamWriter(adultsFile, false)) {
                outputFile.Write(jsonAdults);
            }
            
            //storing users
            string jsonUsers = JsonSerializer.Serialize(Users, new JsonSerializerOptions {
                WriteIndented = true
            });

            using (StreamWriter outputFile = new StreamWriter(usersFile,false)) {
                outputFile.WriteLine(jsonUsers);
            }
        }
    }
}