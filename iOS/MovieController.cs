using DM.MovieApi;
using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace Lab1.iOS
{
	public partial class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		public MovieController()
		{
			MovieDbFactory.RegisterSettings(new MovieDbSettings());
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

			this.Title = "Movie input";
			this.View.BackgroundColor = UIColor.White;

			this._yCoord = StartY;
			var prompt = this.CreatePromptl();

			var movieField = this.CreateMovieField();

			var findMovieButton = this.CreateButton("Find movie");

			var movieLabel = this.CreateLabel();
			findMovieButton.TouchUpInside += async (sender, args) =>
            	{
					movieField.ResignFirstResponder();
					ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);
					movieLabel.Text = response.Results[0].Title;
				};

			this.View.AddSubview(prompt);
			this.View.AddSubview(movieField);
			this.View.AddSubview(findMovieButton);
			this.View.AddSubview(movieLabel);

			//var navigateButton = this.CreateButton("See movie list");
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

		private UILabel CreateLabel()
		{
			var greetingLabel = new UILabel() { Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50) };
			this._yCoord += StepY;
			return greetingLabel;
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

