using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.MovieDbConnection
{
    public static class MovieDbClient
    {

        public static async Task<List<MovieDTO>> getAllMoviesMatchingString(string searchString)
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(searchString);
            List<MovieDTO> results = (from movie in response.Results
                                      select new MovieDTO()
                                      {
                                          ID = movie.Id,
                                          Title = movie.Title,
                                          Year = movie.ReleaseDate.Year.ToString(),
                                          Cast = new List<string>(),
                                          PosterPath = movie.PosterPath,
                                          Overview = movie.Overview
                                      }).ToList();
            foreach (MovieDTO movie in results)
            {
                movie.Cast = await getMovieCastMembersByMovieID(movie.ID);
            }
            return results;
        }

        public static async Task<MovieDetailsDTO> getMovieDetailsByID(int ID)
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiQueryResponse<Movie> response = await movieApi.FindByIdAsync(ID);
            Movie movie = response.Item;
            MovieDetailsDTO movieDetails = new MovieDetailsDTO()
            {
                Title = movie.Title,
                Year = movie.ReleaseDate.Year.ToString(),
                Cast = await getMovieCastMembersByMovieID(ID),
                PosterPath = movie.PosterPath,
                Overview = movie.Overview,
                Runtime = movie.Runtime.ToString(),
				Genres = (from genre in movie.Genres
				          select genre.Name).ToList()			               
            };
            return movieDetails;
        }

        public static async Task<List<MovieDTO>> getTopRatedMovies()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiSearchResponse<MovieInfo> response = await movieApi.GetTopRatedAsync();
            List<MovieDTO> results = (from movie in response.Results
                                      select new MovieDTO()
                                      {
                                          ID = movie.Id,
                                          Title = movie.Title,
                                          Year = movie.ReleaseDate.Year.ToString(),
                                          Cast = new List<string>(),
                                          PosterPath = movie.PosterPath,
                                          Overview = movie.Overview
                                      }).ToList();
            foreach (MovieDTO movie in results)
            {
                movie.Cast = await getMovieCastMembersByMovieID(movie.ID);
            }
            return results;
        }

        private static async Task<List<string>> getMovieCastMembersByMovieID(int movieID)
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiQueryResponse<MovieCredit> r = await movieApi.GetCreditsAsync(movieID);
            if (r.Item == null)
            {
                return new List<string>();
            }
            List<MovieCastMember> list = r.Item.CastMembers.ToList();
            List<string> cast = new List<string>();
            if (list != null)
            {
                foreach (var member in list)
                {
                    if (member != null)
                    {
                        cast.Add(member.Name);
                    }
                }
            }
            return cast;
        }
    }
}