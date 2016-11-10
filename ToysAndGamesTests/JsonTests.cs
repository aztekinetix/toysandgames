using DataRepository;
using DataRepository.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToysAndGamesTests
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void JsonIsLoadedIntoAToysList()
        {
            //Arrange
            
            //Execute
            IEnumerable<Toy> myToys = DataUtils.LoadJsonFile();
            var Toy1 = myToys.First();
            var Toy1Name = Toy1.Id;
            //Assert
            Assert.IsNotNull(myToys);
            Assert.AreEqual(Toy1Name, 1);
            
        }

        [TestMethod]
        public void JsonIsSavedIntoFile()
        {
            //Arrange
            IEnumerable<Toy> myToys = DataUtils.LoadJsonFile();
           
            var listToys = myToys.Cast<Toy>().ToList();
            listToys.Add(new Toy { Id = 4, Name = "Daniel Toy", Description = "For all ages", AgeRestriction = 39, Company = "Sony", Price = 19.99m });
            //Execute
            bool saved = DataUtils.SaveJsonFile(listToys);
            //reload the info from file
            myToys = DataUtils.LoadJsonFile();
            var size = myToys.Count();
            //Assert
            Assert.IsTrue(saved);
            //Assert.AreEqual(size, 4);

        }
    }
}
