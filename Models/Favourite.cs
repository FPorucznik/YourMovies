using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourMovies.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
