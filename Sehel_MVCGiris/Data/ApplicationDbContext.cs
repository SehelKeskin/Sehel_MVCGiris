using Sehel_MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sehel_MVCGiris.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

    
    }



   
}