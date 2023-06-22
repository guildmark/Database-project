using Projekt.Models.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Projekt.Models
{
    public class OurDbContext : DbContext
    {

        public DbSet<UserDetails> UserDetail { get; set; }
    }
}
