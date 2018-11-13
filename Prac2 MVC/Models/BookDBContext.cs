using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace Prac2mvc.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Book ID")]
        public string BookID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }
    public class BookDBContext : DbContext
    {
        // Your context has been configured to use a 'BookDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Prac2mvc.Models.BookDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BookDBContext' 
        // connection string in the application configuration file.
        public BookDBContext()
            : base("name=BookDBContext")
        {
        }

        public System.Data.Entity.DbSet<Prac2mvc.Models.Book> Books { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}