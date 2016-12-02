using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDownload;
using System.Threading;
using System.IO;
using Foundation;
using UIKit;
using Lab1.Models;
using Lab1.MovieDbConnection;
using CoreGraphics;

namespace Lab1.iOS.Controllers
{
    class TopRatedController : UITableViewController
    {
        private List<MovieDTO> _movieList;
        private MovieDbClient movieDbCLient;
        private bool fromTabController;

        public TopRatedController(List<MovieDTO> movieList)
        {
            TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);
            _movieList = movieList;
            movieDbCLient = new MovieDbClient();
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
                activityIndicator.Frame = new CGRect(100, 100, 50, 50);
                activityIndicator.StartAnimating();
                _movieList = await movieDbCLient.getTopRatedMovies();
                TableView.Source = new MovieListSource(_movieList, OnSelectedMovie);
                activityIndicator.RemoveFromSuperview();
                fromTabController = false;
            }
        }

        private async void OnSelectedMovie(int row)
        {
            MovieDbClient movieDbClient = new MovieDbClient();
			StorageClient storage = new StorageClient();
			ImageDownloader downloader = new ImageDownloader(storage);
			CancellationToken token = new CancellationToken();
			string localpath;

			MovieDetailsDTO movie = await movieDbClient.getMovieDetailsByID(_movieList[row].ID);


			if (!string.IsNullOrEmpty(movie.PosterPath))
			{
				localpath = downloader.LocalPathForFilename(movie.PosterPath);
				if (!File.Exists(localpath))
				{
					await downloader.DownloadImage(movie.PosterPath, localpath, token);
				}
				movie.PosterPath = localpath;
			}
			NavigationController.PushViewController(new MovieDetailsController(movie), true);
		}
    }
}