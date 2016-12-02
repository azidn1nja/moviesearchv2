using System;
using UIKit;
using CoreGraphics;
using Lab1.Models;
using Lab1.MovieDbConnection;
using MovieDownload;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Lab1.iOS
{
	public class MovieDetailsController : UIViewController 
	{
		private int _movieid;
		MovieDbClient movieApi;
		private const int HorizontalMargin = 20;
		private const int StartY = 80;
		private const int StepY = 50;
		private int _yCoord;

		public MovieDetailsController(int movieID)
		{
			_movieid = movieID;
			_yCoord = StartY;
			movieApi = new MovieDbClient();
		}
		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Movie info";
			View.BackgroundColor = UIColor.White;

			StorageClient storage = new StorageClient();
			ImageDownloader downloader = new ImageDownloader(storage);
			CancellationToken token = new CancellationToken();
			string localpath;

			MovieDetailsDTO movie = await movieApi.getMovieDetailsByID(_movieid);

			string titleYear = movie.Title + " (" + movie.Year + ")";

			UILabel titleLabel = CreateTitle(titleYear);
			string genreConcat = "";
			if (movie.Genres != null)
			{
				genreConcat = string.Join(", ", movie.Genres);
			}
			string genreRuntime = movie.Runtime + " Min | " + genreConcat;

			UILabel genreRuntimeLabel = CreateGenreRuntimeLabel(genreRuntime);

			UILabel overviewLabel = CreateOverviewLabel(movie.Overview);

			UIImageView posterLabel;
			if (!string.IsNullOrEmpty(movie.PosterPath))
			{
				localpath = downloader.LocalPathForFilename(movie.PosterPath);
				if (!File.Exists(localpath))
				{
					await downloader.DownloadImage(movie.PosterPath, localpath, token);
				}
				movie.PosterPath = localpath;
				posterLabel = CreateImage(movie.PosterPath);
				View.AddSubview(posterLabel);
			}
			View.AddSubview(titleLabel);
			View.AddSubview(genreRuntimeLabel);
			View.AddSubview(overviewLabel);

		}
		private UIImageView CreateImage(string posterpath)
		{
			UIImageView posterView = new UIImageView();
			posterView.Image  = UIImage.FromFile(posterpath);
			posterView.Frame = new CGRect(10, _yCoord, View.Bounds.Width - 255, 35);
			return posterView;

		}
		private UILabel CreateTitle(string titleYear)
		{
			var titleLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = titleYear
			};
			this._yCoord += StepY;
			return titleLabel;
		}
		private UILabel CreateGenreRuntimeLabel(string genreRuntime) 
		{ 
			var genrerunLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = genreRuntime
			};
			this._yCoord += StepY;
			return genrerunLabel;				
		}
		private UILabel CreateOverviewLabel(string overview)
		{
			var overviewLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = overview
			};
			this._yCoord += StepY;
			return overviewLabel;
		}

	}
}
