using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Models.Details;
using Projekt.Models.Methods;
using Microsoft.AspNetCore.Session;
using System.Net.Mail;


namespace Projekt.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDetails ud)
        {
            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.RegisterUserHash(ud, out error);

            ViewBag.amount = i;
            ViewBag.error = error;

            //Create and send a registration mail to the user
            string to = ud.Mail;
            string subject = "Welcome to MOVIE REVIEWS!";
            string body = "Welcome to our new site, we hope you will feel right at home!";
            MailMessage mm = new MailMessage();

            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;

            mm.From = new MailAddress("themarkieone@gmail.com");
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("themarkieone@gmail.com", "inflamesrocks542");
            smtp.Send(mm);

            ViewBag.message = "Confirmation mail has been sent to " + ud.Mail + " successfully!";

            return View("RegisterDONE");
        }
      
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserDetails ud)
        {
            UserMethods um = new UserMethods();
            UserDetails ud2 = new UserDetails();
            
            string error = "";

            ud2 = um.loginUserHash(ud, out error);

            ViewBag.username = ud2.Username;
            ViewBag.userID = ud2.Id;
           
            ViewBag.error = error;

            if(ud2.Username == ud.Username)
            {
                //Create session variables
                HttpContext.Session.SetString("Username", ud2.Username);
                HttpContext.Session.SetInt32("UserID", ud2.Id);

                return View("LoggedIn");
            }
            else
            {
 
                return View();
            }

        }

        public IActionResult UserReviews()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");

            List<MovieReviewDetail> MovieReviewList = new List<MovieReviewDetail>();
            MovieReviewMethods mrm = new MovieReviewMethods();
            string error = "";
            
            MovieReviewList = mrm.GetMovieReviews(userID, out error);
            //ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;
            

            return View(MovieReviewList);
       
        }

        public IActionResult Profile()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");

            UserDetails ud = new UserDetails();
            UserMethods um = new UserMethods();
            string error = "";

            ud = um.GetUser(userID, out error);

            //ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;


            return View(ud);

        }

        [HttpGet]
        public IActionResult Description()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");

            return View();
        }

        [HttpPost]
        public IActionResult Description(UserDetails ud)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");
            UserMethods um = new UserMethods();

            int i = 0;
            string error = "";

            i = um.addUserDescription(ud, userID, out error);
            
            ViewBag.amount = i;
            ViewBag.error = error;
            ViewBag.userID = userID;
            ViewBag.userDesc = ud.Description;

            return RedirectToAction("Profile");
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserDetails ud)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.username = (string)HttpContext.Session.GetString("Username");
            UserMethods um = new UserMethods();
            string error = "";

            int i = um.ChangePassword(userID, ud, out error);

            return RedirectToAction("Profile");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            TempData["filmID"] = id;
            return RedirectToAction("UpdateReview", "Review");
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

        public ActionResult Delete(int id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            MovieReviewDetail mrd = new MovieReviewDetail();
            MovieReviewMethods mrm = new MovieReviewMethods();

            //Need to confirm deletion of review
            TempData["id"] = id;

            int i = 0;
            string error = "";

            i = mrm.DeleteMovieReviewID(id, out error);

            ViewBag.amount = i;
            ViewBag.error = error;

            return RedirectToAction("UserReviews");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Review");
        }

    }
}