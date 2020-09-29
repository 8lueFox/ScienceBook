using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScienceBook.Web.Models;
using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.DAL
{
    public class ScienceBookInitializer : DropCreateDatabaseIfModelChanges<ScienceBookContext>
    {
        protected override void Seed(ScienceBookContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));
            roleManager.Create(new IdentityRole("Moderator"));

            var user = new ApplicationUser { UserName = "kacper.jedrzejewski@int.pl", Email = "kacper.jedrzejewski@int.pl", EmailConfirmed = true };
            string passwd = "CiezkieH1@";

            userManager.Create(user, passwd);
            userManager.AddToRole(user.Id, "Admin");

            var mem = new List<Member>
            {
                new Member{
                    BirthDate = new DateTime(1997,2,14),
                    Email = "kacper.jedrzejewski@int.pl",
                    FirstName = "Kacper",
                    LastName = "Jędrzejewski",
                    Username = "_Ashby",
                    JoinDate = DateTime.Now,
                },
            };
            mem.ForEach(m => context.Members.Add(m));
            context.SaveChanges();
        }
    }
}