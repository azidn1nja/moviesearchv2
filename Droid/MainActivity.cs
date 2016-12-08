using System;
using Android.App;
using Android.Widget;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Views;

namespace Lab1.Droid
{
	using System.Collections.Generic;
	using Android.Hardware.Input;
	using Android.Views.InputMethods;
	using Lab1.Models;
	using Lab1.MovieDbConnection;

	[Activity(Label = "Lab1", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private  List<MovieDTO> _movies;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var button = this.FindViewById<Button>(Resource.Id.movieNameButton);

			var searchText = this.FindViewById<EditText>(Resource.Id.searchText);

			var promptTextView = this.FindViewById<TextView>(Resource.Id.promptTextView);

			button.Click += (sender, args) =>
		    {
			   var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
			   manager.HideSoftInputFromWindow(searchText.WindowToken, 0);
			   var movies = MovieDbClient.getAllMoviesMatchingString(searchText.Text);
			};				
		}
	}
}

