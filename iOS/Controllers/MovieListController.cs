namespace Lab1.iOS
{
	using System.Collections.Generic;
	using UIKit;
    using Models;

	public class MovieListController : UITableViewController
	{
		private List<MovieDTO> _movieList;
		public MovieListController(List <MovieDTO> movieList)
		{
			_movieList = movieList;
		}
		public override void ViewDidLoad()
		{
			View.BackgroundColor = UIColor.White;
			Title = "MovieList";
			TableView.Source = new MovieListSource(_movieList, OnSelectedPerson);
		}

		private void OnSelectedPerson(int row)
		{

			NavigationController.PushViewController(new MovieDetailsController(_movieList[row].ID), true);
		}
	}
}
