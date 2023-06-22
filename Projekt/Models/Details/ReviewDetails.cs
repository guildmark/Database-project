using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class ReviewDetails
    {

        public ReviewDetails() { }
     

        public int Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Film { get; set; }
        public int User { get; set; }
        
      
    }
}
