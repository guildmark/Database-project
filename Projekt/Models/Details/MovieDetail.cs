using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class MovieDetail
    {

        public MovieDetail() { }

        public string Title { get; set; }
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Display(Name = "User ID")]
        public int User { get; set; }
        public int Image { get; set; }


    }
}
