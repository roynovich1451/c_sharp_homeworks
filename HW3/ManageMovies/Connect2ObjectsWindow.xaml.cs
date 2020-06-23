using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for Connect2ObjectsWindow.xaml
    /// </summary>
    public partial class Connect2ObjectsWindow : Window
    {
        private string type1;
        private string type2;
        public Connect2ObjectsWindow(string type1, string type2)
        {
            InitializeComponent();
            this.type1 = type1;
            this.type2 = type2;
            gbObj1.Header = type1;
            gbObj2.Header = type2;
            try
            {
                switch (type1)
                {
                    case "Movie":
                        switch (type2)
                        {
                            case "Actor":
                                using (var ctx = new dbContext())
                                {
                                    lbObj1.ItemsSource = (from m in ctx.Movies
                                                          select m).ToList();
                                    lbObj2.ItemsSource = (from m in ctx.Actors
                                                          where m.Gender == 1
                                                          select m).ToList();
                                }
                                break;
                            case "Actress":
                                using (var ctx = new dbContext())
                                {
                                    lbObj1.ItemsSource = (from m in ctx.Movies
                                                          select m).ToList();
                                    lbObj2.ItemsSource = (from m in ctx.Actors
                                                          where m.Gender == 0
                                                          select m).ToList();
                                }
                                break;
                            case "Director":
                                using (var ctx = new dbContext())
                                {
                                    lbObj1.ItemsSource = (from m in ctx.Movies
                                                          select m).ToList();
                                    lbObj2.ItemsSource = (from m in ctx.Directors
                                                          select m).ToList();
                                }
                                break;
                        }
                        break;
                    case "Actor":
                        using (var ctx = new dbContext())
                        {
                            lbObj2.ItemsSource = (from m in ctx.Movies
                                                  select m).ToList();
                            lbObj1.ItemsSource = (from m in ctx.Actors
                                                  where m.Gender == 1
                                                  select m).ToList();
                        }
                        break;
                    case "Actress":
                        using (var ctx = new dbContext())
                        {
                            lbObj2.ItemsSource = (from m in ctx.Movies
                                                  select m).ToList();
                            lbObj1.ItemsSource = (from m in ctx.Actors
                                                  where m.Gender == 0
                                                  select m).ToList();
                        }
                        break;
                    case "Director":
                        using (var ctx = new dbContext())
                        {
                            lbObj2.ItemsSource = (from m in ctx.Movies
                                                  select m).ToList();
                            lbObj1.ItemsSource = (from m in ctx.Directors
                                                  select m).ToList();
                        }
                        break;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key"))
                {
                    MessageBox.Show("Your pick already in DB", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        #region click
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Movie selectedMovie = null;
            Actor selectedActor = null;
            Director selectedDirector = null;
            try
            {
                if (lbObj1.SelectedItem == null || lbObj2.SelectedItem == null)
                {
                    MessageBox.Show($"Must pick {type1} and {type2}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    switch (type1)
                    {
                        case "Movie":
                            switch (type2)
                            {
                                case "Actor":
                                case "Actress":
                                    selectedMovie = lbObj1.SelectedItem as Movie;
                                    selectedActor = lbObj2.SelectedItem as Actor;
                                handleMovieActor(selectedActor, selectedMovie);
                                break;
                            case "Director":
                                selectedMovie = lbObj1.SelectedItem as Movie;
                                selectedDirector = lbObj2.SelectedItem as Director;
                                handleMovieDirector(selectedDirector, selectedMovie);
                                break;
                            }
                            break;
                        case "Actor":
                        case "Actress":
                            selectedMovie = lbObj2.SelectedItem as Movie;
                            selectedActor = lbObj1.SelectedItem as Actor;
                            handleMovieActor(selectedActor, selectedMovie);
                            break;
                        case "Director":
                            selectedMovie = lbObj2.SelectedItem as Movie;
                            selectedDirector = lbObj1.SelectedItem as Director;
                            handleMovieDirector(selectedDirector, selectedMovie);
                            break;
                    }
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key"))
                {
                    MessageBox.Show("Your pick already in DB", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Handlers
        public void handleMovieDirector(Director selectedDirector, Movie selectedMovie)
        {
            using (var ctx = new dbContext())
            {
                Director dir = (from d in ctx.Directors
                                where d.Id == selectedDirector.Id
                                select d).First();
                Movie movie = (from m in ctx.Movies
                               where m.MovieSerial == selectedMovie.MovieSerial
                               select m).First();
                if (movie.DirectorId != null)
                {
                    if (movie.DirectorId.Equals(dir.Id) == false)
                    {
                        Director currentDir = (from d in ctx.Directors
                                               where d.Id == movie.DirectorId
                                               select d).First();
                        MessageBoxResult res = MessageBox.Show($"'{movie.Title}' currnet director is {currentDir.FirstName} {currentDir.LastName}\n" +
                            $"are you sure you want to replace it with {dir.FirstName} {dir.LastName}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{dir.FirstName} {dir.LastName} is already '{movie.Title}' director", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                movie.DirectorId = dir.Id;
                ctx.SaveChanges();
                MessageBox.Show($"{dir.FirstName} {dir.LastName} is now '{movie.Title}' director", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        } 

        public void handleMovieActor(Actor selectedActor, Movie selectedMovie)
        {
            using (var ctx = new dbContext())
            {
                Actor act = (from a in ctx.Actors
                             where a.Id == selectedActor.Id
                             select a).First();
                Movie movie = (from m in ctx.Movies
                               where m.MovieSerial == selectedMovie.MovieSerial
                               select m).First();
                ctx.ActorMovie.Add(new ActorMovie
                {
                    Actor = act,
                    ActorId = act.Id,
                    MovieSerial = movie.MovieSerial,
                    MovieSerialNavigation = movie
                });
                ctx.SaveChanges();
                MessageBox.Show($"{act.FirstName} {act.LastName} is now one of '{movie.Title}' cast", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion
    }
}
