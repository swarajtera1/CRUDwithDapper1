using CrudService.Services;
using CRUDwithDapper;
using CRUDwithDapper.Controllers;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MyTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


       [Test]
        public async Task AddTest()
        {
            var mockUnit = new Mock<IConfiguration>();

            var myService = new Dapperr(mockUnit.Object);

            CRUDController home = new CRUDController(myService);
            Parameters parameters = new Parameters();
            parameters.Id = 10;
            parameters.Name = "Cena";
            parameters.Age = 30;
            JsonResult result = (JsonResult)await home.Create(parameters); 

            Parameters results = await home.GetById(10);
            Assert.AreEqual("Cena", results.Name);
            Assert.AreEqual(30, results.Age);
             
        }
        [Test]

        public async Task GetTest()
        {

            var mockUnit = new Mock<IConfiguration>();

            var myService = new Dapperr(mockUnit.Object);

            CRUDController home = new CRUDController(myService);


            Parameters result = await home.GetById(10);
            Assert.AreEqual("Cena", result.Name);
            Assert.AreEqual(30, result.Age);


        }

        [Test]
        public async Task UpdateTest()
        {
            var mockUnit = new Mock<IConfiguration>();

            var myService = new Dapperr(mockUnit.Object);

            CRUDController home = new CRUDController(myService);
            Parameters parameters = new Parameters();
            parameters.Id = 10;
            parameters.Name = "John";
            parameters.Age = 26; 

            JsonResult result = (JsonResult)await home.Update(parameters);

            Parameters results = await home.GetById(10);
            Assert.AreEqual("John", results.Name);
             Assert.AreEqual(26, results.Age); 
        }

        [Test]
        public async Task DeleteTest()
        {
            var mockUnit = new Mock<IConfiguration>();

            var myService = new Dapperr(mockUnit.Object);

            CRUDController home = new CRUDController(myService);
          //  int result = await home.Delete(1);


            JsonResult result = (JsonResult)await home.Delete(10);


            var json = JsonConvert.SerializeObject(result.Value);

            var deserializeData = JsonConvert.DeserializeObject<ResponseObject>(json);

            Assert.AreEqual(0, deserializeData.Data);
        }


    }
}