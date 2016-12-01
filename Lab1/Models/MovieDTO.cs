using System.Collections.Generic;

namespace Lab1.Models
{
    public class MovieDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public List<string> Cast { get; set; }

        public string PosterPath { get; set; }

		public string Overview { get; set; }
    }
}
