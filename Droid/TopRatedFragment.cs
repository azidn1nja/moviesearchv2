using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Lab1.Models;

using Fragment = Android.Support.V4.App.Fragment;
using Lab1.MovieDbConnection;
using Newtonsoft.Json;
using Android.Views.InputMethods;

namespace Lab1.Droid
{
    public class TopRatedFragment : Fragment
    {
        private List<MovieDTO> movies;
        private View rootView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            movies = new List<MovieDTO>();
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);

            this.rootView = rootView;

            var listView = rootView.FindViewById<ListView>(Resource.Id.movielistview);
            listView.ItemClick += async (sender, e) =>
            {
                MovieDetailsDTO movie = await MovieDbClient.getMovieDetailsByID(movies[e.Position].ID);

                var intent = new Intent(this.Context, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetails", JsonConvert.SerializeObject(movie));
                StartActivity(intent);
            };

            return rootView;
        }

        public async System.Threading.Tasks.Task LoadTopRatedMovies()
        {
            var listView = rootView.FindViewById<ListView>(Resource.Id.movielistview);
            listView.Adapter = new MovieListAdapter(this.Activity, new List<MovieDTO>());

            var activityIndicator = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);
            activityIndicator.Visibility = ViewStates.Visible;

            movies = await MovieDbClient.getTopRatedMovies();
            activityIndicator.Visibility = ViewStates.Gone;

            listView.Adapter = new MovieListAdapter(this.Activity, movies);
        }
    }
}