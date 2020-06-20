using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
            Oscars = new HashSet<Oscar>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Oscar> Oscars { get; set; }
    }
}
