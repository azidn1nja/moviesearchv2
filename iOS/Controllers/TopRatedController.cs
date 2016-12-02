using System.Collections.Generic;
using UIKit;
using Lab1.Models;
using Lab1.MovieDbConnection;
using CoreGraphics;

namespace Lab1.iOS.Controllers
{
    class TopRatedController : UITableViewController
    {
        private List<MovieDTO> _movieList;
        private bool fromTabController;

        public TopRatedController(List<MovieDTO> movieList)
        {
            TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);
            _movieList = movieList;
            fromTabController = false;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Top Rated Movies";
            fromTabController = true;
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (fromTabController)
            {
                var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
                View.AddSubview(activityIndicator);
                activityIndicator.Frame = new CGRect((UIScreen.MainScreen.Bounds.Width / 2) - 25, (UIScreen.MainScreen.Bounds.Height * 0.30) - 25, 50, 50);
                activityIndicator.StartAnimating();
                _movieList = await MovieDbClient.getTopRatedMovies();
                await ImageSaver.getMoviePosters(_movieList);
                TableView.Source = new MovieListSource(_movieList, OnSelectedMovie);
                activityIndicator.RemoveFromSuperview();
                fromTabController = false;
            }
        }

        public void refreshList()
        {
            fromTabController = true;
            TableView.Source = null;
        }

        private async void OnSelectedMovie(int row)
        {
            MovieDetailsDTO movie = await MovieDbClient.getMovieDetailsByID(_movieList[row].ID);
            if (movie == null)
            {
                return;
            }
            movie.PosterPath = _movieList[row].PosterPath;
            NavigationController.PushViewController(new MovieDetailsController(movie), true);
        }
    }
}