using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Lab1.Models;
using System.Collections.Generic;
using System.Linq;

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

        public async System.Threading.Tasks.Task<List<MovieDTO>> getAllMoviesMatchingString(string searchString)
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
                ApiQueryResponse<MovieCredit> r = await movieApi.GetCreditsAsync(movie.ID);
                List<MovieCastMember> list = r.Item.CastMembers.ToList();
                if (list != null)
                {
                    foreach (var member in list)
                    {
                        movie.Cast.Add(member.Name);
                    }
                }
            }
            return results;
        }
    }
}