using System;
using System.Collections.Generic;

namespace ManageMovies
{
    //TODO have regex check for Properties
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

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public override bool Equals(object obj)
        {
            Director other = obj as Director;
            if (other == null)
            {
                throw new ArgumentException("Argument of Director.Equals must be of type Director");
            }
            return Id.Equals(other.Id);
        }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Oscar> Oscars { get; set; }
    }
}
