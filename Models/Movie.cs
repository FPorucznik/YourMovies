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

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Release Year")]
        [Range(1900, 2100, ErrorMessage = "Select a year between 1900 and 2100")]
        public int ReleaseYear { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string Genre { get; set; }

        public ICollection<Favourite> Favourites { get; set; }

    }
}
