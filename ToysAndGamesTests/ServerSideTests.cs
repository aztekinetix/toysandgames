using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataRepository.Entities;
using Moq;
using System.Collections.Generic;
using DataRepository;
using ToysAndGames.WebAPI.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace ToysAndGamesTests
{
    

    [TestClass]
    public class ServerSideTests
    {
        public readonly IRepository<Toy> MockToysRepository;
        public readonly List<Toy> toysList;
        public ServerSideTests()
        {
                toysList = new List<Toy> {
                new Toy { Id=1, Name="Barbie Developer", Description="Classic Toy",Price=19.99m,AgeRestriction=10,Company="Mattel"},
                new Toy { Id=2, Name="Funko Pop! Elvis", Description="Vinyl figures for collectors",Price=11.99m,AgeRestriction=19,Company="Funko"},
                new Toy { Id=3, Name="Millenium Falcon", Description="Chewie, we are at home!",Price=99.99m,AgeRestriction=100,Company="Mattel"}

            };

            var mockRepo = new Mock<IRepository<Toy>>();
            mockRepo.Setup(call => call.List()).Returns(toysList);
            mockRepo.Setup(call => call.FindById(It.IsAny<int>())).Returns((int i)=>toysList.Find(x=>x.Id==i));
            mockRepo.Setup(call => call.Add(It.IsAny<Toy>()));
            this.MockToysRepository = mockRepo.Object;
        }
       
        [TestMethod]
        public void ReturnsAllToys()
        {
            var toyController = new ToyController(MockToysRepository);
            var toysObtained = toyController.Get() as List<Toy>;
            Assert.AreEqual(toysList, toysObtained);
        }
        //[TestMethod]
        //public void ReturnToyById()
        //{
        //    var toyController = new ToyController(MockToysRepository);
        //    var toyObtained = toyController.Get(2) as Toy;
        //    var toyToCompare = toysList.Find(x => x.Id == 2);
        //    Assert.AreEqual(toyToCompare,toyObtained);
        //}
        [TestMethod]
        public void GetToy_ShouldNotFindProduct()
        {
            var toyController = new ToyController(MockToysRepository);

            var result = toyController.Get(666);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOkMessageOnPost()
        {
            var ToyController = new ToyController(MockToysRepository);
            ToyController.Request = new HttpRequestMessage();
            ToyController.Configuration = new HttpConfiguration();
            HttpResponseMessage expected = ToyController.Post(new Toy { Id = 8, Name = "Millenium Falcoon", Description = "Chewie, we are at home!", Price = 99.99m, AgeRestriction = 100, Company = "Mattel" });
            HttpResponseMessage actual = ToyController.Request.CreateResponse(HttpStatusCode.OK);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void GetBadRequetOnInvalidModel()
        {
            var ToyController = new ToyController(MockToysRepository);
            ToyController.Request = new HttpRequestMessage();
            ToyController.Configuration = new HttpConfiguration();
            ToyController.ModelState.AddModelError("Myerror", new Exception());
            HttpResponseMessage expected = ToyController.Post(new Object() as Toy);

            Assert.IsTrue(expected.ToString().Contains("Bad Request"));
        }
    }
}
