using System;

namespace Lab1.iOS
{
	using System.Collections.Generic;
	using UIKit;
	public class MovieListController : UITableViewController
	{
		private List<string> _movieList;
		public MovieListController(List <string> movieList)
		{
			_movieList = movieList;
		}
		public override void ViewDidLoad()
		{
			this.View.BackgroundColor = UIColor.White;
			this.Title = "MovieList";
			this.TableView.Source = new MovieListSource(this._movieList);
		}
	}
}
