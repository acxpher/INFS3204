using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIService.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Genre { get; set; }
        public int Year { get; set; }
    }
}