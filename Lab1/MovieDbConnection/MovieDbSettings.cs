using DM.MovieApi;

namespace Lab1.MovieDbConnection
{
	public class MovieDbSettings : IMovieDbSettings
	{
		private const string apiKey = "1fa4f193b00b3121c0ed925acc3c56af";
		private const string apiUrl = "http://api.themoviedb.org/3/";
		string IMovieDbSettings.ApiUrl
		{
			get { return apiUrl; }
		}
		string IMovieDbSettings.ApiKey
		{
			get { return apiKey; }
		}
			
		public MovieDbSettings()
		{
		}

	}
}
