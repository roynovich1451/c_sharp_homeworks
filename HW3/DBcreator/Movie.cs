using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MovieSerial { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Director Director { get; set; }
        public string Country { get; set; }
        public decimal ImdbScore { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; }
        public Oscar Oscar { get; set; }
    }
}
