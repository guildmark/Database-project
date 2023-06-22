using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Projekt.Models
{
    public class ImageDetails
    {

        public ImageDetails() { }

        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Upload File")]
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
