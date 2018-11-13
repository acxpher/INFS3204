using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    public class MovieController : ApiController
    {


        static readonly IMovieRepository repository = new MovieRepository();

        public IEnumerable<Movie> GetAllMovies()
        {
            return repository.GetAll();
        }

        public Movie GetMovie(int id)
        {
            Movie item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        public HttpResponseMessage PostMovie(Movie item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Movie>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        public void PutMovie(int id, Movie item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteMovie(int id)
        {
            Movie item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }

        public IEnumerable<Movie> GetMoviesByGenre(String genre)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Genre, genre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
