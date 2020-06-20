using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class Movie
    {
        public Movie()
        {
            ActorMovie = new HashSet<ActorMovie>();
        }

        public string MovieSerial { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string DirectorId { get; set; }
        public string Country { get; set; }
        public decimal ImdbScore { get; set; }

        public virtual Director Director { get; set; }
        public virtual Oscar Oscars { get; set; }
        public virtual ICollection<ActorMovie> ActorMovie { get; set; }
    }
}
