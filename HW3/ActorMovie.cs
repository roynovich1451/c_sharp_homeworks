using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    public class ActorMovie
    {
        public string ActorId { get; set; }
        public Actor Actor { get; set; }
        public string MovieSerial { get; set; }
        public Movie Movie { get; set; }

    }
}
