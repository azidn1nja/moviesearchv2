namespace Lab1.iOS.Controllers
{
	using System.Collections.Generic;
	using UIKit;
	using Models;
	using MovieDbConnection;
	using MovieDownload;
	using System.Threading;
	using System.IO;

	public class MovieListController : UITableViewController
	{
		private List<MovieDTO> _movieList;

		public MovieListController(List <MovieDTO> movieList)
		{
			_movieList = movieList;
		}
		public override void ViewDidLoad()
		{
            NavigationController.SetNavigationBarHidden(false, true);
            Title = "Movie List";
            View.BackgroundColor = UIColor.White;
			TableView.Source = new MovieListSource(_movieList, OnSelectedMovie);
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
