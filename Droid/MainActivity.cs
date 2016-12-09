using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Lab1.MovieDbConnection;
using Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Lab1.Droid
{
    using Android.Content;
    using Android.Hardware.Input;
    using Android.Views;
    using Android.Views.InputMethods;
    using MovieDownload;
    using System.Threading;

    [Activity(Theme = "@style/MovieSearchTheme" ,Label = "Lab1", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

        private static StorageClient storage = new StorageClient();
        private static ImageDownloader downloader = new ImageDownloader(storage);
        private static CancellationToken token = new CancellationToken();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            var movieEditText = this.FindViewById<EditText>(Resource.Id.movieEditText);

            Button button = FindViewById<Button>(Resource.Id.findMovieButton);

            var activityIndicator = FindViewById<ProgressBar>(Resource.Id.progressBar);


            button.Click += async delegate
            {
                activityIndicator.Visibility = ViewStates.Visible;
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

                List<MovieDTO> movies = await MovieDbClient.getAllMoviesMatchingString(movieEditText.Text);

                var intent = new Intent(this, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);

                activityIndicator.Visibility = ViewStates.Gone;
            };
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "My Toolbar";
        }
	}
}

