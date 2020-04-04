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
            }
            try
            {
                Author a = new Author(tbAFN.Text, tbALN.Text, 1);
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
                int index = Authors.IndexOf(a);
                if (index != NOTFOUND)
                {
                    Authors[index].Published += 1;
                } else
                {
                    Authors.Add(a);
                }
            
            }
            catch (WrongIsbnException wie)
            {
                MessageBox.Show(wie.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpAm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpPr_Click(object sender, RoutedEventArgs e)
        {
           
        }
        #endregion
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
    }
}
