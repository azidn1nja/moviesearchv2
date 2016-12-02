using System.Collections.Generic;

namespace Lab1.Models
{
    public class MovieDetailsDTO
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string PosterPath { get; set; }

        public string Overview { get; set; }

        public string Runtime { get; set; }

		public List<string> Genres { get; set; }
    }
}
