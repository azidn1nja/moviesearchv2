using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class MovieInfo
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public string Cast { get; set; }

        public string PosterPath { get; set; }

		public string Overview { get; set; }
    }
}
