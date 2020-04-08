using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomExtensions;


namespace HW1wpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Authors = new ObservableCollection<Author>();
            Books = new ObservableCollection<Book>();
            lbBooks.ItemsSource = Books;
        }
        private ObservableCollection<Author> Authors;
        private ObservableCollection<Book> Books;
        const int NOTFOUND = -1;
        #region buttons_fanctionality
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!allTextBoxFilled(gBookDetails))
            {
                MessageBox.Show("You must fill all textboxes under Book details", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Author a = new Author(tbAFN.Text, tbALN.Text, 1);
                int index = Authors.IndexOf(a);
                if (index != NOTFOUND)
                {
                    Authors[index].Published += 1;
                }
                else
                {
                    Authors.Add(a);
                }
                Book b = new Book(tbIsbn.Text, tbTitle.Text, a, int.Parse(tbNOC.Text), decimal.Parse(tbPrice.Text));
                if (Books.Contains(b))
                {
                    throw new IsbnInUseException("The book ISBN, " + b.Isbn + ", is in use at library");
                }
                else
                {
                    Books.Add(b);
                    /*TODO: sort the book list 
                     * will learn next lesson
                     * Books - new ObservableCollection<Book>(Books.OrderBy(i => i.CompareTo()));
                     */
                }
                rtbHistory.AppendText("Added "+ b.Name, "Green");
                clearBooksTextBoxes(gBookDetails);
            }
            catch (WrongIsbnException wie)
            {
                MessageBox.Show(wie.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            /*TODO: will learn after holiday*/ 
        }

        private void btnUpAm_Click(object sender, RoutedEventArgs e)
        {
            if (lbBooks.SelectedItem == null)
            {
                MessageBox.Show("You must select a Book to change number of copies", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Book book2change = lbBooks.SelectedItem as Book;
            if (!int.TryParse(tbNOC.Text, out int tmp) || string.IsNullOrEmpty(tbNOC.Text))
            {
                MessageBox.Show("You must give natural number at 'copies' textbox","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            int value = int.Parse(tbNOC.Text);
            int oldAmount = Books[Books.IndexOf(book2change)].Copies;
            Books[Books.IndexOf(book2change)].Copies = value;
            tbDisCopies.Text = tbNOC.Text;
            rtbHistory.AppendText("change amount of  " + book2change.Name + " from " +oldAmount+ " to "+ tbNOC.Text, "Blue");
        }

        private void btnUpPr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbBooks.SelectedItem == null)
                {
                    MessageBox.Show("You must select a Book to change his price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Book book2change = lbBooks.SelectedItem as Book;
                if (!decimal.TryParse(tbPrice.Text, out decimal tmp) || string.IsNullOrEmpty(tbPrice.Text))
                {
                    throw new ArgumentException("You must give natural number at 'copies' textbox");
                }
                if (book2change.Price < tmp)
                {
                    throw new ArgumentException("Book's new Price can only be lower then his old one");
                }
                decimal oldPrice = Books[Books.IndexOf(book2change)].Price;
                Books[Books.IndexOf(book2change)].Price = tmp;
                tbDisPrice.Text = tbPrice.Text;
                rtbHistory.AppendText("change price of  " + book2change.Name + "to" + tbPrice.Text, "Blue");
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        #endregion
        #region side_Functions
        private bool allTextBoxFilled(Panel p)
        {
            foreach (var item in p.Children)
            {
                if (item is TextBox)
                {
                    TextBox tb = item as TextBox;
                    if(tb.IsEnabled == true && string.IsNullOrEmpty(tb.Text))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void clearBooksTextBoxes(Panel p)
        {
            foreach (var item in p.Children)
            {
                if (item is TextBox)
                {
                    TextBox tb = item as TextBox;
                    tb.Text = null;
                }
            }
        }
        #endregion

        private void lbBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selected = lbBooks.SelectedItem as Book;
            Author auth = selected.Auth as Author;
            tbDisAuthor.Text = auth.FirstName + " " + auth.LastName;
            tbDisAuthorBooks.Text = auth.Published.ToString();
            tbDisCopies.Text = selected.Copies.ToString();
            tbDisPrice.Text = selected.Price.ToString();
        }
    }
}
