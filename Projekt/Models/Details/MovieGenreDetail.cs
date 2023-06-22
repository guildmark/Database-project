using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models.Details
{
    public class MovieGenreDetail
    {

        public MovieGenreDetail() { }

        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int User { get; set; }
        public int Image { get; set; }
        public string Genre { get; set; }
    }
}
