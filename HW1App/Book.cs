using System;
using System.Text.RegularExpressions;

namespace HW1wpfApp
{
    internal class Book : IComparable
    {
        private static string isbnPattern = @"^[0-9]{13}$";

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
                if (!Regex.Match(value, isbnPattern).Success)
                {
                    throw new WrongIsbnException("ISBN must contains 13 numbers only");
                }
                isbn = value;
            }
        }

        public Author Auth
        {
            get
            {
                return auth;
            }
            set
            {
                auth = value;
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
                    throw new ArgumentException("Copies must get a natural number");
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
                    throw new ArgumentException("Book price can't be negative nubmer");
                }
                price = value;
            }
        }

        #endregion properties

        #region constuctors

        public Book(string isbn, string name, Author auth, int copies, decimal price)
        {
            Isbn = isbn;
            Name = name;
            Auth = auth;
            Copies = copies;
            Price = price;
        }

        #endregion constuctors

        #region overrides

        public override bool Equals(object obj)
        {
            Book other = obj as Book;
            if (other == null)
            {
                throw new ArgumentException("Equals argument must be of type Book");
            }
            return Isbn.Equals(other.Isbn);
        }

        public override string ToString()
        {
            return Name + ", " + Isbn;
        }

        #endregion overrides

        public int CompareTo(object obj)
        {
            Book b = obj as Book;
            if (b == null)
            {
                throw new ArgumentException("CompareTo must get argument of type Book");
            }
            else
            {
                return Name.CompareTo(b.Name);
            }
        }
    }
}