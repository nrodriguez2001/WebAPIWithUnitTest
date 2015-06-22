using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

using System.Web.Http.Routing;

using Moq;

using VevoAPI.Controllers;
using VevoAPI.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VevoAPI.Test
{
    [TestClass]
    public class TestArtist
    {
        const int Id = 1;

        [TestMethod]
        public void GetAllArtistsShouldReturnAllArtists()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.GetAll()).Returns(this.GetTestArtists);
            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetAllArtists();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetArtistShouldReturnNotFound()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            
            repoMock.Setup(r => r.Get(Id)).Returns((Artist) null);
            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetArtist(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetArtistShouldReturnNotFoundAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Get(Id)).Throws(new IOException());
           
            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetArtist(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetArtistShouldReturnOneArtists()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();

            repoMock.Setup(r => r.Get(Id)).Returns(this.artist);

            var controller = new ArtistsController(
               repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetArtist(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.Found, result.StatusCode);
        }

        [TestMethod]
        public void PostArtistReturnsCreatedArtist()
        {
            //Arrange
            var repoMock = new Mock<IArtistRepository>();
 
            repoMock.Setup(r => r.Add(this.artist)).Returns(this.artist);

            var controller = new ArtistsController(repoMock.Object);

            SetupControllerForTests(controller, HttpMethod.Post);

            // Act
            var result = controller.PostArtist(this.artist);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }

        [TestMethod]
        public void PostArtistShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Add(this.artist)).Throws(new IOException());

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Post);

            //Act
            var result = controller.PostArtist(this.artist);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void PutArtistUpdatesRepository()
        {
            //Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Update(this.artist)).Returns(this.artist);

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Put);

            // Act
            var response = controller.PutArtist(this.artist);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void PutArtistShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Update(this.artist)).Throws(new IOException());

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Put);

            //Act
            var result = controller.PutArtist(this.artist);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void DeleteArtistCallsRepositoryRemove()
        {
            //Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Remove(Id)).Returns(0);

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller,HttpMethod.Delete);

            // Act
            var response = controller.DeleteArtist(Id);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void DeleteArtistCallsRepositoryRemoveFailed()
        {
            //Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Remove(Id)).Returns(1);

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Delete);

            // Act
            var response = controller.DeleteArtist(Id);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteArtistShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IArtistRepository>();
            repoMock.Setup(r => r.Remove(Id)).Throws(new IOException());

            var controller = new ArtistsController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Delete);

            //Act
            var result = controller.DeleteArtist(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        private static void SetupControllerForTests(ApiController controller, HttpMethod method)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(method, "http://localhost/api/artists/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "artists" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
        }

        private List<Artist> GetTestArtists()
        {
            var testArtists = new List<Artist>();
            testArtists.Add(new Artist { artist_id = 1, urlSafeName = "Demo1", name = "Demo10" });
            testArtists.Add(new Artist { artist_id = 2, urlSafeName = "Demo2", name = "Demo20" });
            testArtists.Add(new Artist { artist_id = 3, urlSafeName = "Demo3", name = "Demo30" });
            testArtists.Add(new Artist { artist_id = 4, urlSafeName = "Demo4", name = "Demo40" });

            return testArtists;
        }

        private readonly Artist artist = new Artist { artist_id = 1, urlSafeName = "Demo1", name = "Demo10" };
    }
}
