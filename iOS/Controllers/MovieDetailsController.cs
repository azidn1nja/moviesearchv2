using System;
using UIKit;
using CoreGraphics;
using Lab1.Models;
using Lab1.MovieDbConnection;
using MovieDownload;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Lab1.iOS.Controllers
{
	public class MovieDetailsController : UIViewController 
	{
		private MovieDetailsDTO movie;
		private const int HorizontalMargin = 20;
		private const int StartY = 50;
		private const int StepY = 170;
		private int _yCoord;

		public MovieDetailsController(MovieDetailsDTO movie)
		{
			this.movie = movie;
			_yCoord = StartY;
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Movie info";
			View.BackgroundColor = UIColor.White;


			if (!string.IsNullOrEmpty(movie.PosterPath))
			{
				UIImageView posterLabel;
				posterLabel = CreateImage(movie.PosterPath);
				View.AddSubview(posterLabel);
			}
			string titleYear = movie.Title + " (" + movie.Year + ")";

			UILabel titleLabel = CreateTitle(titleYear);

			UILabel overviewLabel = CreateOverviewLabel(movie.Overview);

			string genreConcat = "";
			string genreRuntime = "";
			if (movie.Genres != null)
			{
				genreConcat = string.Join(", ", movie.Genres);
				if (!string.IsNullOrEmpty(movie.Runtime))
				{
					genreRuntime = movie.Runtime + " Min | " + genreConcat;
				}
				else
				{
					genreRuntime = genreConcat;
				}
			}
			else 
			{
				genreRuntime = movie.Runtime;
			}

			UILabel genreRuntimeLabel = CreateGenreRuntimeLabel(genreRuntime);

			View.AddSubview(titleLabel);
			View.AddSubview(genreRuntimeLabel);
			View.AddSubview(overviewLabel);

		}
		private UIImageView CreateImage(string posterpath)
		{
			UIImageView posterView = new UIImageView();
			posterView.Image  = UIImage.FromFile(posterpath);
			posterView.Frame = new CGRect(200, 80, View.Bounds.Width - 220, 150);
			return posterView;

		}
		private UILabel CreateTitle(string titleYear)
		{
			var titleLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin * 2, this._yCoord + 20, this.View.Bounds.Width - 205, 100),
				Text = titleYear,
				Lines = 4,
				Highlighted = true,
				TextAlignment = UITextAlignment.Center
			};
			this._yCoord = 240;
			return titleLabel;
		}
		private UILabel CreateGenreRuntimeLabel(string genreRuntime) 
		{ 
			var genrerunLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, 198, 150, 50),
				Text = genreRuntime,
				TextAlignment = UITextAlignment.Center,
				Highlighted = true
			};
			genrerunLabel.Font = UIFont.FromName("HelveticaNeue-Italic", 11);
			this._yCoord += StepY;
			return genrerunLabel;				
		}
		private UILabel CreateOverviewLabel(string overview)
		{
			var overviewLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width-30, 100),
				Text = overview,
				Lines = 10 
					
			};
			overviewLabel.Font = UIFont.FromName("AppleSDGothicNeo-Light", 10);
			this._yCoord += StepY;


			return overviewLabel;
		}

	}
}
