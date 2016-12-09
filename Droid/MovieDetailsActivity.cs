using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Lab1.Models;
using Square.Picasso;

namespace Lab1.Droid
{
    [Activity(Theme = "@style/MovieSearchTheme", Label = "MovieDetailsActivity")]
    public class MovieDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MovieDetails);

            var jsonStr = Intent.GetStringExtra("movieDetails");
            var movie = JsonConvert.DeserializeObject<MovieDetailsDTO>(jsonStr);

            // Create your application here
            FindViewById<TextView>(Resource.Id.titleAndYear).Text = movie.Title + " (" + movie.Year + ")";
            FindViewById<TextView>(Resource.Id.overview).Text = movie.Overview;
            FindViewById<TextView>(Resource.Id.runtimeAndGenres).Text = movie.Runtime + " Min | " + string.Join(" ,", movie.Genres);

            Picasso.With(this).Load("http://image.tmdb.org/t/p/w500" + movie.PosterPath).Into(FindViewById<ImageView>(Resource.Id.poster));
        }
    }
}