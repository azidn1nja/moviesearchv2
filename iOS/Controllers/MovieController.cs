using UIKit;
using CoreGraphics;
using Lab1.Models;
using Lab1.MovieDbConnection;
using MovieDownload;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Lab1.iOS.Controllers
{
	public partial class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 180;

		private const int StepY = 50;

		private int _yCoord;

		private List<MovieDTO> _movies;

        private MovieDbClient movieDbClient;

		public MovieController(List<MovieDTO> movies)
		{
            TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
            _movies = movies;
            movieDbClient = new MovieDbClient();
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(true, true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            NavigationController.SetNavigationBarHidden(false, true);
        }

        public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            StorageClient storage = new StorageClient();
            ImageDownloader downloader = new ImageDownloader(storage);
            CancellationToken token = new CancellationToken();

			View.BackgroundColor = UIColor.White;

			_yCoord = StartY;

            var logoView = SetupLogo();

			var movieField = CreateMovieField();

			var findMovieButton = CreateButton("Search");

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
                    _movies = await movieDbClient.getAllMoviesMatchingString(movieField.Text);
                    await ImageSaver.getMoviePosters(_movies);
					NavigationController.PushViewController(new MovieListController(_movies), true);
                    findMovieButton.Enabled = true;
                    movieField.Text = string.Empty;
                    activityIndicator.RemoveFromSuperview();
                }
                else
                {
                    UIView.Animate(1, () =>
                    {
                        logoView.Layer.BorderColor = new CGColor(244, 44, 44, 0.9f);
                    }, () =>
                    {
                        UIView.Animate(1, () =>
                        {
                            logoView.Layer.BorderColor = new CGColor(0, 0, 0, 0.5f);
                        });
                    });
                }
            };
            View.AddSubview(logoView);
            View.AddSubview(movieField);
            View.AddSubview(errorLabel);
            View.AddSubview(findMovieButton);
        }

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		private UIButton CreateButton(string title)
		{
			var button = new Button();
            double buttonWidth = UIScreen.MainScreen.Bounds.Width * 0.9;
            double buttonLeftMargin = UIScreen.MainScreen.Bounds.Width * 0.05;
            double buttonTopMargin = UIScreen.MainScreen.Bounds.Width * 0.9;
            button.Frame = new CGRect(buttonLeftMargin, buttonTopMargin, buttonWidth, 50);

            button.Layer.BorderWidth = 1;
            button.Layer.BorderColor = new CGColor(0, 0, 0, 0.5f);
            button.Layer.CornerRadius = 5;

            button.SetTitleColor(UIColor.FromRGBA(0, 0, 0, 0.8f), UIControlState.Normal);
            button.SetTitleColor(UIColor.White, UIControlState.Highlighted);
            button.SetTitleColor(UIColor.FromRGBA(0, 0, 0, 0.3f), UIControlState.Disabled);

            button.SetTitle(title, UIControlState.Normal);
            button.Font = UIFont.FromName("AppleSDGothicNeo-Light", 18);
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
            double fieldWidth = UIScreen.MainScreen.Bounds.Width * 0.9;
            double fieldLeftMargin = UIScreen.MainScreen.Bounds.Width * 0.05;
            double fieldTopMargin = UIScreen.MainScreen.Bounds.Width * 0.7;
            var movieField = new UITextField()
			{
				Frame = new CGRect(fieldLeftMargin, fieldTopMargin, fieldWidth, 50),
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Movie Title",
                TextAlignment = UITextAlignment.Center
			};
			this._yCoord += StepY;
			return movieField;
		}

        private UIImageView SetupLogo()
        {
            var logoView = new UIImageView();
            logoView.Image = UIImage.FromFile("logo");
            // We want the logo to be 90% of the screen
            double logoWidth = UIScreen.MainScreen.Bounds.Width * 0.90;
            // and then a 5% margin on either side
            double logoLeftMargin = UIScreen.MainScreen.Bounds.Width * 0.05;
            // we want it a top margin of 30%
            double logoTopMargin = UIScreen.MainScreen.Bounds.Height * 0.3;
            logoView.Frame = new CGRect(logoLeftMargin, logoTopMargin, logoWidth, logoWidth * 0.1833);

            return logoView;
        }
	}
}

