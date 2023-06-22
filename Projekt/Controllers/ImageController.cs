using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Models.Methods;

namespace Projekt.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Show(int id)
        {

            ImageDetails imdetail = new ImageDetails();
            ImageMethods im = new ImageMethods();

            int i = 0;
            string error = "";
            imdetail = im.getImage(id, out error);

            return File(imdetail.Image, "image/jpg");
        }


    }
}