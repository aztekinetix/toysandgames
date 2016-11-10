using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataRepository.Entities;
using DataRepository;
using System.Linq;

namespace ToysAndGamesTests
{
    /// <summary>
    /// I used this Unit to test my Repo, however, due the restricted time, it was tied to a file located
    /// on my machine under c:/test/JsonDatasoure
    /// As this was intended for Web, I am removing this code
    /// </summary>
    [TestClass]
    public class RepoTests
    {
        private MyRepository myToyRepository = new MyRepository();

        [TestMethod]
        public void CanListToys()
        {

            var toysList = myToyRepository.List();
         
            //Assert
            Assert.IsNotNull(toysList);

        }
        [TestMethod]
        public void CanAddToy()
        {
            int sizeInitial = 0;
            if (myToyRepository.List() != null) { 
                sizeInitial = myToyRepository.List().Count();
            }
            myToyRepository.Add(new Toy { Id = 4, Name = "Daniel Test", AgeRestriction = 20, Company = "Intel", Description = "My description", Price = 1399.19m });
            
            int sizeFinal = myToyRepository.List().Count();
            int sizeExpected = sizeInitial + 1;

            ////Assert
            Assert.AreEqual(sizeExpected, sizeFinal);

        }

        [TestMethod]
        public void CanDeleteToy()
        {
            Toy myEntityToDelete = myToyRepository.List().OrderByDescending(x=>x.Id).First();
            int sizeInitial = myToyRepository.List().Count();
            myToyRepository.Delete(myEntityToDelete);
            //var toysList = myToyRepository.List;
            int sizeFinal = myToyRepository.List().Count();
            int sizeExpected = sizeInitial - 1;

            ////Assert
            Assert.AreEqual(sizeExpected, sizeFinal);

        }

        [TestMethod]
        public void CanUpdateToy()
        {
            Toy myEntityToUpdate = myToyRepository.List().First();
            int idOfEntityToUpdate = myEntityToUpdate.Id;
            string nameOfEntityToUpdate = myEntityToUpdate.Name;
            myEntityToUpdate.Name = "Barbie Vengadora" + DateTime.Now;

            myToyRepository.Update(myEntityToUpdate);
            Toy myUpdatedEntity=myToyRepository.FindById(idOfEntityToUpdate);
            string nameOfTheUpdatedEntity = myUpdatedEntity.Name;

            ////Assert
            Assert.AreNotEqual(nameOfEntityToUpdate, nameOfTheUpdatedEntity);
            
        }
    }
}
