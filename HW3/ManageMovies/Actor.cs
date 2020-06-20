using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class Actor
    {
        public Actor()
        {
            ActorMovie = new HashSet<ActorMovie>();
            OscarsBestActor = new HashSet<Oscar>();
            OscarsBestActress = new HashSet<Oscar>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearBorn { get; set; }
        public int Gender { get; set; }

        public virtual ICollection<ActorMovie> ActorMovie { get; set; }
        public virtual ICollection<Oscar> OscarsBestActor { get; set; }
        public virtual ICollection<Oscar> OscarsBestActress { get; set; }
    }
}
