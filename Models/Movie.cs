using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourMovies.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        [Display(Name ="Release Year")]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }

    }
}
