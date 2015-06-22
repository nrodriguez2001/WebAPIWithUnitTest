using System.Collections.Generic;
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
    using System.IO;

    [TestClass]
    public class TestVideos
    {
        const int Id = 1;

        [TestMethod]
        public void GetAllVideosShouldReturnAllVideos()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.GetAll()).Returns(this.GetTestVideos);
            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetAllVideos();

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetVideoShouldReturnNotFound()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();

            repoMock.Setup(r => r.Get(Id)).Returns((Video)null);
            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetVideo(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetVideoShouldReturnNotFoundAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Get(Id)).Throws(new IOException());

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetVideo(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetVideoShouldReturnOneVideos()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();

            repoMock.Setup(r => r.Get(Id)).Returns(this.video);

            var controller = new VideosController(
               repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Get);

            //Act
            var result = controller.GetVideo(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.Found, result.StatusCode);
        }

        [TestMethod]
        public void PostVideoReturnsCreatedVideo()
        {
            //Arrange
            var repoMock = new Mock<IVideoRepository>();

            repoMock.Setup(r => r.Add(this.video)).Returns(this.video);

            var controller = new VideosController(repoMock.Object);

            SetupControllerForTests(controller, HttpMethod.Post);

            // Act
            var result = controller.PostVideo(this.video);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }

        [TestMethod]
        public void PostVideoShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Add(this.video)).Throws(new IOException());

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Post);

            //Act
            var result = controller.PostVideo(this.video);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void PutVideoUpdatesRepository()
        {
            //Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Update(this.video)).Returns(this.video);

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Put);

            // Act
            var response = controller.PutVideo(this.video);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void PutVideoShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Update(this.video)).Throws(new IOException());

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Put);

            //Act
            var result = controller.PutVideo(this.video);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void DeleteVideoCallsRepositoryRemove()
        {
            //Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Remove(Id)).Returns(0);

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Delete);

            // Act
            var response = controller.DeleteVideo(Id);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void DeleteVideoCallsRepositoryRemoveFailed()
        {
            //Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Remove(Id)).Returns(1);

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Delete);

            // Act
            var response = controller.DeleteVideo(Id);

            // Assert
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteVideoShouldReturnBadRequestAfterThrowError()
        {
            // Arrange
            var repoMock = new Mock<IVideoRepository>();
            repoMock.Setup(r => r.Remove(Id)).Throws(new IOException());

            var controller = new VideosController(repoMock.Object);
            SetupControllerForTests(controller, HttpMethod.Delete);

            //Act
            var result = controller.DeleteVideo(Id);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        private static void SetupControllerForTests(ApiController controller, HttpMethod method)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(method, "http://localhost/api/videos/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "videos" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
        }

        private List<Video> GetTestVideos()
        {
            var testVideos = new List<Video>();
            testVideos.Add(new Video { video_id = 1, artist_id = 18, isrc = "1111111111", urlSafeTitle = "illuminati_1", VideoTitle = "Illuminati 1" });
            testVideos.Add(new Video { video_id = 2, artist_id = 18, isrc = "2222222222", urlSafeTitle = "illuminati_2", VideoTitle = "Illuminati 2" });
            testVideos.Add(new Video { video_id = 3, artist_id = 18, isrc = "3333333333", urlSafeTitle = "illuminati_3", VideoTitle = "Illuminati 3" });
            testVideos.Add(new Video { video_id = 4, artist_id = 18, isrc = "4444444444", urlSafeTitle = "illuminati_4", VideoTitle = "Illuminati 4" });

            return testVideos;
        }

        private readonly Video video = new Video { video_id = 4, artist_id = 18, isrc = "4444444444", urlSafeTitle = "illuminati_4", VideoTitle = "Illuminati 4" };

    }
}
