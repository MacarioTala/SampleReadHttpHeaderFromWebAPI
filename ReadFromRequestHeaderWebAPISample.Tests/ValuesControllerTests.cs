using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReadFromRequestHeaderWebAPISample.Controllers;

namespace ReadFromRequestHeaderWebAPISample.Tests
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void GetHeaderInfoShouldReturnHeaderInfo()
        {
            //Arrange
            // Set the header of the request to have a login key
            const string expected = "AAAAA";
            var controller = new ValuesController {Request = new HttpRequestMessage()};
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,new HttpConfiguration());
            controller.Request.Headers.Add("LoginKey",expected);

            //Act
            IHttpActionResult result = controller.GetHeaderInfo();

            //IhttpActionResult returns a task which needs to be executed
            //Then get the message body
            var getHeaderInfoTask = result.ExecuteAsync(new CancellationToken());
            var messageFromTask = getHeaderInfoTask.Result;
            
            var readRequestTask = messageFromTask.Content.ReadAsStringAsync();
            var actual =  readRequestTask.Result.Replace("\"","");// Task 'helpfully' adds quotes when returning content

            //Assert
            Assert.AreEqual(expected,actual);   
        }
    }
}
