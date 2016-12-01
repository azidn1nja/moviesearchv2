using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
	public class Movies
	{
		private List<MovieDTO> _movies;

		public Movies()
		{
		}

		public Movies(List<MovieDTO> movies)
		{
			_movies = movies;

		}
		public List<MovieDTO> Films
		{
			get { return _movies; }
			set { _movies = value; }
		}
	}		
}
