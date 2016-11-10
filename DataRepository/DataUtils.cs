using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataRepository.Entities;
using Newtonsoft.Json;
using System.Reflection;
using System.Web;
using System.Diagnostics;

namespace DataRepository
{
    public static class DataUtils
    {

        static DataUtils()
        {
            //if (!File.Exists(Location))
            //{
            //    SeedJsonFile();
            //}

        }
        //private const string FILENAME = @"C:\BegASPNET\ToysAndGames\JsonDataSource.json";
        private static string Location { get {
                if (HttpContext.Current == null)
                {
                    return @"C:\BegASPNET\ToysAndGames\JsonDataSource.json"; //For testing without HTTP Context
                }
                else  // this is being executed on web app
                {
                return HttpContext.Current.Server.MapPath("~/JsonDataSource/JsonDataSource.json"); //when running from the MVC app
            }

        }
        }

        private static void SeedJsonFile()
        {
            //TODO: generate overwrite the JsonFile when it is executed for the first time
            var seedData = new List<Toy> {
                new Toy { Id=1, Name="Barbie Developer", Description="Classic Toy",Price=19.99m,AgeRestriction=10,Company="Mattel"},
                new Toy { Id=2, Name="Funko Pop! Elvis", Description="Vinyl figures for collectors",Price=11.99m,AgeRestriction=19,Company="Funko"},
                new Toy { Id=3, Name="Millenium Falcon", Description="Chewie, we are at home!",Price=99.99m,AgeRestriction=100,Company="Mattel"},

            };

            SaveJsonFile(seedData);

            return;
        }

        
        public static IEnumerable<Toy> LoadJsonFile()
        {
            List<Toy> toys;
            
            try
            {
                using (StreamReader r = new StreamReader(Location))
                {
                    string json = r.ReadToEnd();
                    toys = JsonConvert.DeserializeObject<List<Toy>>(json);
                }

                return toys;
            }
            
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool SaveJsonFile(IEnumerable<Toy> myToyList)
        {
            if (myToyList != null && File.Exists(Location))
            {
                try
                {
                    //serialize the list to store
                    var json = JsonConvert.SerializeObject(myToyList);
                    //save json into a file overwriting it
                        File.WriteAllText(Location, json);
                        return true;
                    
                    
                } catch(Exception e)
                {
                    throw new Exception("Error ocurred when saving", e.InnerException);
                }
                
            }

            return false;
        }

    }
}
