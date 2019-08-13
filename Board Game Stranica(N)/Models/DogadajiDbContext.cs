using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica_N_.Models
{
    public class DogadajiDbContext : DbContext
    {
        public DbSet<Dogadaj> Dogadaji { get; set; }
    }
}