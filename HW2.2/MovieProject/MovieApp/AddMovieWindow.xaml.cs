using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        public Movie newMovie;
        public AddMovieWindow()
        {
            InitializeComponent();
        }

        public void generateMovie()
        {
            List<string> movieActors = new List<string>();
        } 
    }
}