using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class Oscar
    {
        public int Year { get; set; }
        public string BestActorId { get; set; }
        public string BestActressId { get; set; }
        public string BestDirectorId { get; set; }
        public string MovieSerial { get; set; }

        public virtual Actor BestActor { get; set; }
        public virtual Actor BestActress { get; set; }
        public virtual Director BestDirector { get; set; }
        public virtual Movie MovieSerialNavigation { get; set; }
    }
}
