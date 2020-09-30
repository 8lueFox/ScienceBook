using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.DAL
{
    public class ScienceBookContext : DbContext
    {
        public ScienceBookContext()
            : base("DefaultConnection")
        { 
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<ScienceClub> ScienceClubs { get; set; }
        public DbSet<CategoryOfScienceClub> CategoriesOfScienceClub { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}