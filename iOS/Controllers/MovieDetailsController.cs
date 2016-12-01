using System;
using UIKit;
using CoreGraphics;
using Lab1.Models;
namespace Lab1.iOS
{
	public class MovieDetailsController : UIViewController 
	{
		private MovieInfo _movie; 
		public MovieDetailsController(MovieInfo movie)
		{
			_movie = movie;
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

		}
	}
}
