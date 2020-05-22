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
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        ObservableCollection<MoviePerson> moviePeoples;
        public MainWindow()
        {
            InitializeComponent();
            moviePeoples = new ObservableCollection<MoviePerson>();
        }

        private void add_actor_click(object sender, RoutedEventArgs e)
        {
            AddMoviePersonWindow addActorWindow = new AddMoviePersonWindow();
            addActorWindow.ShowDialog();
            addActorWindow.Close();
            moviePeoples.Add(addActorWindow.newPerson);
        }
    }
}
