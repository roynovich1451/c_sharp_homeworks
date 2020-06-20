using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class ActorMovie
    {
        public string ActorId { get; set; }
        public string MovieSerial { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie MovieSerialNavigation { get; set; }
    }
}
