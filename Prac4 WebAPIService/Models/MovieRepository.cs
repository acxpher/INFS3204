using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebAPIService.Models
{
    public class MovieRepository: IMovieRepository
    {
        private List<Movie> movies = new List<Movie>();
        private int _nextId = 1;

        public MovieRepository()
        {
            Add(new Movie { Name = "The Glass Castle", Genre = "Drama", Year = 2017 });
            Add(new Movie { Name = "Wind River", Genre = "Crime", Year = 2017 });
            Add(new Movie { Name = "The Matrix", Genre = "Sci-fi", Year = 1999 });
        }

        public IEnumerable<Movie> GetAll()
        {
            return movies;
        }

        public Movie Get(int id)
        {
            return movies.Find(p => p.Id == id);
        }

        public Movie Add(Movie item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            movies.Add(item);
            return item;
        }

        public void Remove(int id)
        {
           movies.RemoveAll(p => p.Id == id);
        }

        public bool Update(Movie item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = movies.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            movies.RemoveAt(index);
            movies.Add(item);
            return true;
        }
    }



}
