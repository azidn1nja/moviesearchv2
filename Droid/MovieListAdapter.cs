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
using Lab1.Models;
using Java.IO;
using Android.Graphics;
using Square.Picasso;

namespace Lab1.Droid
{
    class MovieListAdapter : BaseAdapter<MovieDTO>
    {
        private Activity _context;
        private List<MovieDTO> movies;

        public MovieListAdapter(Activity context, List<MovieDTO> movies)
        {
            this._context = context;
            this.movies = movies;
        }

        public override int Count
        {
            get
            {
                return movies.Count;
            }
        }

        public override MovieDTO this[int position]
        {
            get
            {
                return movies[position];
            }
        }

        public override long GetItemId(int position)
        {
            return movies[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
            }

            var movie = movies[position];
            view.FindViewById<TextView>(Resource.Id.titleAndYear).Text = movie.Title + " (" + movie.Year + ")";
            view.FindViewById<TextView>(Resource.Id.castMembers).Text = getFirstThreeCastMembers(movie.Cast);

            Picasso.With(_context).Load("http://image.tmdb.org/t/p/w92" + movie.PosterPath).Into(view.FindViewById<ImageView>(Resource.Id.poster));

            return view;
        }

        private string getFirstThreeCastMembers(List<string> castMembers)
        {
            string result = "";
            if (castMembers != null)
            {
                int count = Math.Min(castMembers.Count, 3);
                List<string> temp = (from member in castMembers
                                     select member).Take(count).ToList();
                result = string.Join(", ", temp);
            }
                return result;
        }
    }
}