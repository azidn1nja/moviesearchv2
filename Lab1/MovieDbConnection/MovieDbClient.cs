using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.MovieDbConnection
{
    public class MovieDbClient
    {
        private IApiMovieRequest movieApi;

        public MovieDbClient()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        public async Task<List<MovieDTO>> getAllMoviesMatchingString(string searchString)
        {
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

        public async Task<MovieDetailsDTO> getMovieDetailsByID(int ID)
        {
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

        private async Task<List<string>> getMovieCastMembersByMovieID(int movieID)
        {
            ApiQueryResponse<MovieCredit> r = await movieApi.GetCreditsAsync(movieID);
            List<MovieCastMember> list = r.Item.CastMembers.ToList();
            List<string> cast = new List<string>();
            if (list != null)
            {
                foreach (var member in list)
                {
                    cast.Add(member.Name);
                }
            }
            return cast;
        }
    }
}