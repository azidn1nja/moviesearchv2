using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
	public class Movies
	{
		private List<string> _movies;

		public Movies()
		{
		}

		public Movies(List<string> movies)
		{
			_movies = movies;

		}
		public List<string> Films
		{
			get { return this._movies; }
			set { this._movies = value; }
		}
	}		
}
