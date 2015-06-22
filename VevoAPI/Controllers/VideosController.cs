using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using VevoAPI.Models;

namespace VevoAPI.Controllers
{
    public class VideosController : ApiController
    {
        readonly IVideoRepository repository;
        private readonly ModelFactory modelfactory;
        public VideosController(IVideoRepository repository)
        {
            this.repository = repository;
            this.modelfactory = new ModelFactory();
        }

        public HttpResponseMessage GetAllVideos()
        {
            var videos = repository.GetAll();
            var response = this.Request.CreateResponse(HttpStatusCode.OK, videos);
            return response;
        }

        public HttpResponseMessage GetVideo(int id)
        {
            try
            {
                var item = this.repository.Get(id);
                var response = item == null ?
                    this.Request.CreateResponse(HttpStatusCode.NotFound) :
                    this.Request.CreateResponse(HttpStatusCode.Found, this.modelfactory.Create(item));
                return response;
            }
            catch (IOException)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage PostVideo(Video item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                item = this.repository.Add(item);
                if (item == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                var response = this.Request.CreateResponse(HttpStatusCode.Created, this.modelfactory.Create(item));

                if (this.Url != null)
                {
                    var uri = this.Url.Link("DefaultApi", new { id = item.video_id });
                    if (uri != null)
                    {
                        response.Headers.Location = new Uri(uri);
                    }
                }

                return response;
            }
            catch (IOException)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage PutVideo(Video video)
        {
            try
            {
                var item = this.repository.Update(video);
                var response = this.Request.CreateResponse(item.artist_id == 0 ?
                    HttpStatusCode.NotFound : HttpStatusCode.OK, this.modelfactory.Create(item));

                return response;
            }
            catch (IOException)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage DeleteVideo(int id)
        {
            try
            {
                var response = this.repository.Remove(id) == 0 ?
                                this.Request.CreateResponse(HttpStatusCode.OK, true) :
                                this.Request.CreateResponse(HttpStatusCode.BadRequest, false);

                return response;
            }
            catch (IOException)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}
