using DM.MovieApi;
using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Linq;
using Lab1.Models;
using System.Collections.Generic;

namespace Lab1.iOS
{
	public partial class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private Movies _movies;

		public MovieController()
		{
			MovieDbFactory.RegisterSettings(new MovieDbSettings());
			_movies = new Movies();
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

			var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);

			findMovieButton.TouchUpInside += async (sender, args) =>
            {
				findMovieButton.Enabled = false;
				this.View.AddSubview(activityIndicator);
				activityIndicator.Frame = new CGRect(HorizontalMargin, _yCoord - 50, View.Bounds.Width - HorizontalMargin, 50);
				activityIndicator.StartAnimating();
				movieField.ResignFirstResponder();
                ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);
                this._movies.Films = (from movie in response.Results
                                      select new Models.MovieInfo()
                                      {
                                          ID = movie.Id,
                                          Title = movie.Title,
                                          Year = movie.ReleaseDate.Year.ToString(),
                                          Cast = ""
                                        }).ToList();
                foreach (Models.MovieInfo m in _movies.Films)
                {
                    ApiQueryResponse<MovieCredit> r = await movieApi.GetCreditsAsync(m.ID);
                    List<MovieCastMember> list = r.Item.CastMembers.Take(3).ToList();
                    var count = 0;
                    while (count < list.Count)
                    {
                        m.Cast += list[count].Name;
                        count += 1;
                    }
                    /*if (list.Count >= 3)
                    {
                        m.Cast = list[0].Name + ", " + list[1].Name + ", " + list[2].Name;
                    }*/
                }
				NavigationController.PushViewController(new MovieListController(_movies.Films), true);
				findMovieButton.Enabled = true;
				activityIndicator.RemoveFromSuperview();
			};

			View.AddSubview(prompt);
			View.AddSubview(movieField);
			View.AddSubview(findMovieButton);
			View.AddSubview(movieLabel);

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

