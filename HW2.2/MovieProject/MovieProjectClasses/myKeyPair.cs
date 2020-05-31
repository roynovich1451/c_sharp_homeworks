using System;

namespace MovieProjectClasses
{
    public class MyKeyPair : IComparable
    {
        public string Name { set; get; }
        public int Year { set; get; }
        #region constructors
        public MyKeyPair(string s, int i)
        {
            Name = s;
            Year = i;
        }
        #endregion
        public override bool Equals(object obj)
        {
            if (!(obj is MyKeyPair mp))
            {
                throw new Exception("Equals must get MyKeyPair argument");
            }
            return this.Name.Equals(mp.Name) &&
                this.Year.Equals(mp.Year);
        }
        public override string ToString()
        {
            return $"{Name} {Year.ToString()}";
        }
        public int CompareTo(object obj)
        {
            if (!(obj is MyKeyPair mp))
            {
                throw new ArgumentException("compareTo must get MyKeyPair");
            }
            int ret = this.Year.CompareTo(mp.Year);
            if (ret != 0) return ret;
            return this.Name.CompareTo(mp.Name);
        }
    }

}
