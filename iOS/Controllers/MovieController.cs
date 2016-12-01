using UIKit;
using CoreGraphics;
using Lab1.Models;
using Lab1.MovieDbConnection;
using System;
using System.Collections.Generic;
using MovieDownload;
using System.Threading;
using System.IO;

namespace Lab1.iOS
{
	public partial class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private Movies _movies;

        private MovieDbClient movieDbClient = new MovieDbClient();

		public MovieController()
		{
			
			_movies = new Movies();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            StorageClient storage = new StorageClient();
            ImageDownloader downloader = new ImageDownloader(storage);
            CancellationToken token = new CancellationToken();

            Title = "Movie input";
			View.BackgroundColor = UIColor.White;

			_yCoord = StartY;
			var prompt = CreatePromptl();

			var movieField = CreateMovieField();

			var findMovieButton = CreateButton("Find movie");

            var errorLabel = CreateLabel(true);
            errorLabel.Hidden = true;

			var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);

			findMovieButton.TouchUpInside += async (sender, args) =>
            {
                if (!string.IsNullOrEmpty(movieField.Text))
                {
                    findMovieButton.Enabled = false;
                    View.AddSubview(activityIndicator);
                    activityIndicator.Frame = new CGRect(HorizontalMargin, _yCoord - 50, View.Bounds.Width - HorizontalMargin, 50);
                    activityIndicator.StartAnimating();
                    movieField.ResignFirstResponder();
                    _movies.Films = await movieDbClient.getAllMoviesMatchingString(movieField.Text);

                    NavigationController.PushViewController(new MovieListController(_movies.Films), true);
                    string localpath;
                    foreach (MovieDTO movie in _movies.Films)
                    {
                        if (movie.PosterPath != null)
                        {
                            localpath = downloader.LocalPathForFilename(movie.PosterPath);
                            if (!File.Exists(localpath))
                            {
                                await downloader.DownloadImage(movie.PosterPath, localpath, token);
                            }
                            movie.PosterPath = localpath;
                        }
                    }
                    findMovieButton.Enabled = true;
                    movieField.Text = string.Empty;
                    activityIndicator.RemoveFromSuperview();
                }
                else
                {
                    errorLabel.Text = "Searchstring is required!";

                    errorLabel.Alpha = 0;
                    errorLabel.Hidden = false;
                    UIView.Animate(1, () =>
                    {
                        errorLabel.Alpha = 1;
                    }, () =>
                    {
                        UIView.Animate(1, () =>
                        {
                            errorLabel.Alpha = 0.0f;
                        });
                    });
                }
            };

            View.AddSubview(prompt);
            View.AddSubview(movieField);
            View.AddSubview(errorLabel);
            View.AddSubview(findMovieButton);
        }

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		private UILabel CreatePromptl()
		{
			var prompt = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = "Enter name of the movie: "
			};
			this._yCoord += StepY;
			return prompt;
		}
		private UIButton CreateButton(string title)
		{
			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 15 * HorizontalMargin, 50);
			button.SetTitle(title, UIControlState.Normal);
			this._yCoord += StepY;
			return button;
		}

		private UILabel CreateLabel(bool errorLabel)
		{
            var label = new UILabel() { Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50) };
			this._yCoord += StepY;
            if (errorLabel)
            {
                label.TextColor = UIColor.Red;
            }
			return label;
		}

		private UITextField CreateMovieField()
		{
			var movieField = new UITextField()
			{
				Frame =
										new CGRect(
											HorizontalMargin,
											this._yCoord,
											this.View.Bounds.Width - 16 * HorizontalMargin,
											50),
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Movie Title"
			};
			this._yCoord += StepY;
			return movieField;
		}
	}
}

