using System;

namespace HW1
{
    class Author
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private int published;

        #region properties
        public int Published
        {
            get
            {
                return published;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Author book published must be natural number");
                }
                published = value;
            }
        }
        #endregion
        #region constructors
        public Author(string fn, string ln, int published)
        {
            FirstName = fn;
            LastName = ln;
            Published = published;
        }
        #endregion
        #region overrides
        public override string ToString()
        {
            return FirstName + " " + LastName + ", number of published books:" + Published;
        }

        internal Author Clone()
        {
            return new Author(this.FirstName, this.LastName, this.Published);
        }

        public override bool Equals(object obj)
        {
            Author other = obj as Author;
            if(other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type Author");
            }
            return this.FirstName.Equals(other.FirstName) &&
                this.LastName.Equals(other.LastName) && 
                this.Published.Equals(other.Published);
        }
        #endregion
    }
}
