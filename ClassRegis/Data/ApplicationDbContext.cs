using System;
using System.Collections.Generic;
using System.Text;
using ClassRegis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassRegis.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Students> Students { get; set; }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<StudyClasses> StudyClasses { get; set; }
    }
}
