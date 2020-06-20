using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    public enum Gender { Male, Female }
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearBorn { get; set; }
        public Gender Gender { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; }
    }
}
