using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourMovies.ViewModels
{
    public class DetailsViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Display(Name = "Account type:")]
        public string AccountType { get; set; }


        [Display(Name = "Number of favourited movies:")]
        public int FavouriteNumber { get; set; }
    }
}
