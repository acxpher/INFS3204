﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIService.Models
{
   public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int id);
        Movie Add(Movie item);
        void Remove(int id);
        bool Update(Movie item);

    }
}
