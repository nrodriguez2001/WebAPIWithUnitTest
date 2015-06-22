using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using VevoAPI.Models;

namespace VevoAPI.Controllers
{
    public class ArtistsController : ApiController
    {
        readonly IArtistRepository repository;
        private readonly ModelFactory modelfactory;
        public ArtistsController(IArtistRepository repository)
        {
            this.repository = repository;
            this.modelfactory = new ModelFactory();
        }

        public HttpResponseMessage GetAllArtists()
        {
            var artists = this.repository.GetAll();
            var response = this.Request.CreateResponse(HttpStatusCode.OK, artists);
            return response;
        }

        public HttpResponseMessage GetArtist(int id)
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

        public HttpResponseMessage PostArtist(Artist item)
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
                    var uri = this.Url.Link("DefaultApi", new { id = item.artist_id });
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

        public HttpResponseMessage PutArtist(Artist artist)
        {
            try
            {
                var item = this.repository.Update(artist);
                if (item == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var response = this.Request.CreateResponse(item.artist_id == 0 ?
                    HttpStatusCode.NotFound : HttpStatusCode.OK, this.modelfactory.Create(item));

                return response;
            }
            catch (IOException)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage DeleteArtist(int id)
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
