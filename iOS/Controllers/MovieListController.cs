namespace Lab1.iOS.Controllers
{
	using System.Collections.Generic;
	using UIKit;
	using Models;
	using MovieDbConnection;

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
            MovieDetailsDTO movie = await MovieDbClient.getMovieDetailsByID(_movieList[row].ID);
            if (movie == null)
            {
                return;
            }
            movie.PosterPath = _movieList[row].PosterPath;
            NavigationController.PushViewController(new MovieDetailsController(movie), true);
        }

	}
}
