﻿namespace Lab1.iOS
{
	using System.Collections.Generic;
	using UIKit;
    using Lab1.Models;

	public class MovieListController : UITableViewController
	{
		private List<MovieInfo> _movieList;
		public MovieListController(List <MovieInfo> movieList)
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
			
			var okAlertController = UIAlertController.Create("Person selected", this._movieList[row].Title, UIAlertControllerStyle.Alert);

			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

			this.PresentViewController(okAlertController, true, null);
		}
	}
}
