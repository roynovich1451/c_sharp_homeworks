using System;
using System.Collections.Generic;
using System.Text;

namespace MovieProjectClasses
{
    public class MyKeyPair
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
            if (obj is MyKeyPair)
            {
                throw new Exception("Equals must get mtKeyPair argument");
            }
            MyKeyPair mk = obj as MyKeyPair;
            return this.Name.Equals(mk.Name) &&
                this.Year.Equals(mk.Year);
        }
        public override string ToString()
        {
            return $"{Name} {Year.ToString()}";
        }
    }

}
