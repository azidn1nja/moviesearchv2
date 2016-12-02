namespace Lab1.iOS.Controllers
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
            NavigationController.SetNavigationBarHidden(false, true);
            Title = "Movie List";
            View.BackgroundColor = UIColor.White;
			TableView.Source = new MovieListSource(_movieList, OnSelectedMovie);
		}

		private void OnSelectedMovie(int row)
		{

			NavigationController.PushViewController(new MovieDetailsController(_movieList[row].ID), true);
		}
	}
}
