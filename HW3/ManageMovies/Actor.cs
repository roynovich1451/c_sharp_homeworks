using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace ManageMovies
{
    //TODO have regex check for Properties
    public partial class Actor
    {
        public Actor()
        {
            ActorMovies = new HashSet<ActorMovie>();
            OscarsBestActors = new HashSet<Oscar>();
            OscarsBestActresses = new HashSet<Oscar>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearBorn { get; set; }
        public int Gender { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
        public virtual ICollection<Oscar> OscarsBestActors { get; set; }
        public virtual ICollection<Oscar> OscarsBestActresses { get; set; }
    }
}
