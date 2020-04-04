using System;
using System.Collections.Generic;
using System.Text;

namespace HW1wpfApp
{
    class Book: IComparable
    {
        private string isbn;
        public string Name { get; set; }
        private Author auth;
        private int copies;
        private decimal price;
        #region properties
        public string Isbn
        {
            get
            {
                return isbn;
            }
            set
            {
                if (!isNumber(value))
                {
                    throw new WrongIsbnException("ISBN must contains only numbers");
                }
                if (value.Length != 13)
                {
                    throw new WrongIsbnException("ISBN must have 13 digits");
                }
                isbn = value;
            }
        }
        public Author Auth
        {
            get
            {
                return auth.Clone();
            }
            set
            {
                auth = value.Clone();
            }
        }
        public int Copies
        {
            get
            {
                return copies;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("copies must get a natural number");
                }
                copies = value;
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("book price can't be negative nubmer");
                }
                price = value;
            }
        }
        #endregion
        #region constuctors
        public Book(string isbn, string name, Author auth, int copies, decimal price)
        {
            Isbn = isbn;
            Name = name;
            Auth = auth;
            Copies = copies;
            Price = price;
        }
        #endregion
        #region overrides
        public override bool Equals(object obj)
        {
            Book other = obj as Book;
            if (other == null)
            {
                throw new ArgumentException("Equals argument must be of type Book");
            }
            return this.Isbn.Equals(other.Isbn);
        }
        public override string ToString()
        {
            return Name + ", " + Isbn;
        }
        public override int GetHashCode()
        {
            return int.Parse(Isbn);
        }
        #endregion

        private bool isNumber(string str)
        {
            foreach(char c in str)
            {
                if(c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
        public int CompareTo(object obj)
        {
            Book b = obj as Book;
            if (b == null)
            {
                throw new ArgumentException("compareTo must get argument of type Book");
            }
            else
            {
                return this.Name.CompareTo(b.Name);
            }
        }
    }
}