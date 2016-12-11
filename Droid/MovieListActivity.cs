using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lab1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Lab1.MovieDbConnection;

namespace Lab1.Droid
{
    [Activity(Theme = "@style/MovieSearchTheme", Label = "Movie List")]
    public class MovieListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MovieList);

            // Create your application here
            var jsonStr = Intent.GetStringExtra("movieList");
            var movies = JsonConvert.DeserializeObject<List<MovieDTO>>(jsonStr);

            var listView = FindViewById<ListView>(Resource.Id.movielistview);
            listView.Adapter = new MovieListAdapter(this, movies);

            listView.ItemClick += async (sender, e) =>
            {
                MovieDetailsDTO movie = await MovieDbClient.getMovieDetailsByID(movies[e.Position].ID);

                var intent = new Intent(this, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetails", JsonConvert.SerializeObject(movie));
                StartActivity(intent);
            };

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "List";
        }
    }
}