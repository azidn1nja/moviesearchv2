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

using Fragment = Android.Support.V4.App.Fragment;
using Android.Views.InputMethods;
using Lab1.Models;
using Lab1.MovieDbConnection;
using Newtonsoft.Json;

namespace Lab1.Droid
{
    public class MovieSearchInputFragment : Fragment
    {

        private View rootView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootView = inflater.Inflate(Resource.Layout.MovieSearchInput, container, false);

            this.rootView = rootView;

            var movieEditText = rootView.FindViewById<EditText>(Resource.Id.movieEditText);

            Button button = rootView.FindViewById<Button>(Resource.Id.findMovieButton);

            var activityIndicator = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);


            button.Click += async delegate
            {
                activityIndicator.Visibility = ViewStates.Visible;
                button.Enabled = false;
                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

                List<MovieDTO> movies = await MovieDbClient.getAllMoviesMatchingString(movieEditText.Text);

                if (movies != null && movies.Count > 0)
                {
                    var intent = new Intent(this.Context, typeof(MovieListActivity));
                    intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                    this.StartActivity(intent);
                }

                activityIndicator.Visibility = ViewStates.Gone;
                button.Enabled = true;
            };

            return rootView;
        }

        public void hideInput()
        {
            var movieEditText = rootView.FindViewById<EditText>(Resource.Id.movieEditText);
            var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);
        }
    }
}