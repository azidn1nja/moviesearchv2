using System;
using UIKit;
using CoreGraphics;
using Lab1.Models;
namespace Lab1.iOS
{
	public class MovieDetailsController : UIViewController 
	{
		private MovieDTO _movie; 
		public MovieDetailsController(MovieDTO movie)
		{
			_movie = movie;
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


		}
	}
}
