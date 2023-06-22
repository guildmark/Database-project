using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Models.Details;
using Projekt.Models.Methods;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Projekt.Controllers
{
    public class ReviewController : Controller
    {
        /*
        public IActionResult Index()
        {

            List<MovieDetail> MovieList = new List<MovieDetail>();
            MovieMethods mm = new MovieMethods();
            string error = "";
            MovieList = mm.GetMoviesWithDataSet(out error);
            //ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;

            return View(MovieList);

        }
        */

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            List<MovieReviewDetail> MovieReviewList = new List<MovieReviewDetail>();
            MovieReviewMethods mrm = new MovieReviewMethods();
            string error = "";
            MovieReviewList = mrm.GetMovieReviews(out error);
            //ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;
            

            return View(MovieReviewList);
        }

        [HttpGet]
        public IActionResult InsertReview()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            List<GenreDetails> GenreList = new List<GenreDetails>();
            GenreMethods gm = new GenreMethods();

            string error = "";

            GenreList = gm.GetGenres(out error);
            ViewBag.genres = GenreList;
             

            return View();
        }

        [HttpPost]
        public IActionResult InsertReview(MovieReviewDetail mrd)
        {

            int genreID = Convert.ToInt32(mrd.Genre);

            ViewBag.username = HttpContext.Session.GetString("Username");

            MovieReviewMethods mrm = new MovieReviewMethods();
            int i = 0;
            string error = "";
            

            if(HttpContext.Session.GetInt32("UserID") != null)
            {
                int userID = (int)HttpContext.Session.GetInt32("UserID");
                mrd.User = userID;

                ViewBag.user = userID;
            }
       

            
            
             i = mrm.InsertMovieReview2(genreID, mrd, out error);
             ViewBag.error = error;
             ViewBag.amount = i;





            return RedirectToAction("UserReviews", "User");
            
        }

        [HttpGet]
        public IActionResult UpdateReview()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            return View();
        }
        
        [HttpPost]
        public IActionResult UpdateReview(MovieReviewDetail mrd)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            MovieReviewMethods mrm = new MovieReviewMethods();
            int i = 0;
            string error = "";
            int filmID = (int)TempData["filmID"];

            i = mrm.UpdateMovieReviews(filmID, mrd, out error);

            
            ViewBag.amount = i;
            ViewBag.error = error;

            return RedirectToAction("UserReviews", "User");
        }

        [HttpGet]
        public ActionResult SearchReview()
        {

            return View("Index");
        }

        [HttpPost]
        public ActionResult SearchReview(string searchString)
        {

            return View();
        }



        public ActionResult Details(string title)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            MovieReviewDetail mrd = new MovieReviewDetail();
            MovieReviewMethods mrm = new MovieReviewMethods();
            mrd = mrm.GetSingleMovieReview(title, out string error);

            ViewBag.error = error;

            return View(mrd);
        }


        [HttpGet]
        public ActionResult InsertGenre()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");


            return View();
        }
        [HttpPost]
        public ActionResult InsertGenre(GenreDetails genre)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            GenreMethods gm = new GenreMethods();
            int i = 0;
            string error = "";

            i = gm.InsertGenre(genre, out error);

            ViewBag.error = error;
            ViewBag.amount = i;


            return RedirectToAction("InsertReview");
        }
    }
}