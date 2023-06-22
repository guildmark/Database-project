using Projekt.Models.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class ViewModelMovieReview
    {

        public IEnumerable<MovieReviewDetail> MovieReviewLists { get; set; }
        public IEnumerable<ReviewDetails> ReviewDetailList { get; set; }

        public IEnumerable<MovieDetail> MovieDetailList { get; set; }
    }
}
