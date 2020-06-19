using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    public class Oscar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Year { get; set; }
        public Actor BestActor { get; set; }
        public Actor BestActress { get; set; }
        public Director BestDirector { get; set; }
        public string MovieSerial { get; set; }
        public Movie BestMotionPicture { get; set; }




    }
}
