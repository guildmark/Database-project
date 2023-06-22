using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models.Details
{
    public class MovieReviewDetail
    {
        public MovieReviewDetail() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Movie title is required!")]
        [Display(Name ="Movie Title")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Wrong number of characters!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Release year is required!")]
        [Display(Name = "Release Year")]
        
        public int ReleaseYear { get; set; }
        public int User { get; set; }
        public int Image { get; set; }
        [Required]
        [Range(0, 5)]
        public int Grade { get; set; }
        [StringLength(300, ErrorMessage = "Description can not have more than 300 characters!")]
        public string Description { get; set; }
        public string Genre { get; set;}

        public int GenreID { get; set; }
        public string Actor { get; set;}
        public string Username { get; set; }
    }
}
