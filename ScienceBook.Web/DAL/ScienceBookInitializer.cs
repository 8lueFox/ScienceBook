using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScienceBook.Web.Models;
using ScienceBook.Web.Models.DbModels;
using ScienceBook.Web.Models.Statics;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
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

            var univ = new List<University>
            {
                new University { Name = "Uniwersytet Przyrodniczo-Humanistyczny"},
                new University { Name = "Uniwersytet Warszawski"},
                new University { Name = "Uniwersytet Kardynała Stefana Wyszyńskiego"},
                new University { Name = "Warszawski Uniwersytet Medyczny"},
                new University { Name = "Uniwersytet Wrocławski"},
                new University { Name = "Uniwersytet Ślaski"},
                new University { Name = "Politechnika Lubelska"},
                new University { Name = "Politechnika Warszawska"},
                new University { Name = "Politechnika Poznańska"}
            };

            univ.ForEach(u => context.Universities.Add(u));

            var depart = new List<Department>
            {
               new Department {Name = "Wydział Nauk Ścisłych i Przyrodniczych", Shortcut = "WNŚiP", University = univ[0] },
               new Department {Name = "Wydział Sztuki Pięknej", Shortcut = "WSP", University = univ[1] },
               new Department {Name = "Wydział Budownictwa i Architektury", Shortcut = "WBIA", University = univ[5] },
               new Department {Name = "Wydział Elektrotechniki i Informatyki", Shortcut = "WEII", University = univ[7] },
               new Department {Name = "Wydział Budownictwa i Architektury", Shortcut = "WBIA", University = univ[2] },
            };

            depart.ForEach(d => context.Departments.Add(d));

            var fields = new List<FieldOfStudy>
            {
                new FieldOfStudy { Name = "Informatyka"},
                new FieldOfStudy { Name = "Matematyka"},
                new FieldOfStudy { Name = "Dziennikarstwo"},
                new FieldOfStudy { Name = "Biologia sądowa"},
                new FieldOfStudy { Name = "Chemia"},
                new FieldOfStudy { Name = "Kryminologia"},
                new FieldOfStudy { Name = "Ogrodnictwo"},
                new FieldOfStudy { Name = "Mechatronika"},
                new FieldOfStudy { Name = "Logistyka"},
            };

            fields.ForEach(f => context.FieldsOfStudies.Add(f));

            var catOfSc = new List<CategoryOfScienceClub>
            {
                new CategoryOfScienceClub{ Name = "Koło informatyczne"},
                new CategoryOfScienceClub{ Name = "Koło dziennikarskie"},
                new CategoryOfScienceClub{ Name = "Koło taneczne"}
            };

            catOfSc.ForEach(c => context.CategoriesOfScienceClub.Add(c));
            context.SaveChanges();

            var sc = new List<ScienceClub>
            {
                new ScienceClub
                {
                    CategoryOfScienceClubID = 1,
                    CreationDate = DateTime.Now,
                    DepartmentID = 1,
                    Name = "KNS Genbit",
                    Logo = Imager.ImageToByteArray(Image.FromFile(@"F:\!!! INZYNIERKA !!!\ScienceBook Seriously Last Version XD\ScienceBook\ScienceBook.Web\App_Data\images\genbit.png")),
                    Tasks = new List<Task>()
                },
                new ScienceClub
                {
                    CategoryOfScienceClubID = 2,
                    CreationDate = DateTime.Now,
                    DepartmentID = 3,
                    Name = "Papugi",
                    Logo = Imager.ImageToByteArray(Image.FromFile(@"F:\!!! INZYNIERKA !!!\ScienceBook Seriously Last Version XD\ScienceBook\ScienceBook.Web\App_Data\images\papugi.jpg")),
                    Tasks = new List<Task>()
                },
                new ScienceClub
                {
                    CategoryOfScienceClubID = 3,
                    CreationDate = DateTime.Now,
                    DepartmentID = 4,
                    Name = "Łabędzie",
                    Logo = Imager.ImageToByteArray(Image.FromFile(@"F:\!!! INZYNIERKA !!!\ScienceBook Seriously Last Version XD\ScienceBook\ScienceBook.Web\App_Data\images\labedzie.jpg")),
                    Tasks = new List<Task>()
                },
            };

            sc.ForEach(s => context.ScienceClubs.Add(s));
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role{Name = "Członek", ScienceClubID = 1 },
                new Role{Name = "Sekretarz", ScienceClubID = 1 },
                new Role{Name = "Vice-przewodniczący", ScienceClubID = 1 },
                new Role{Name = "Przewodniczący", ScienceClubID = 1 },
                new Role{Name = "Opiekun koła", ScienceClubID = 1 },
                new Role{Name = "Członek", ScienceClubID = 2 },
                new Role{Name = "Sekretarz", ScienceClubID = 2 },
                new Role{Name = "Vice-przewodniczący", ScienceClubID = 2 },
                new Role{Name = "Przewodniczący", ScienceClubID = 2 },
                new Role{Name = "Opiekun koła", ScienceClubID = 2 },
                new Role{Name = "Członek", ScienceClubID = 3 },
                new Role{Name = "Sekretarz", ScienceClubID = 3 },
                new Role{Name = "Vice-przewodniczący", ScienceClubID = 3 },
                new Role{Name = "Przewodniczący", ScienceClubID = 3 },
                new Role{Name = "Opiekun koła", ScienceClubID = 3 }
            };

            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var catsOfTask = new List<CategoryOfTask>
            {
                new CategoryOfTask
                {
                    Name = "Pilne",
                    ScienceClub = sc[0],
                },
                new CategoryOfTask
                {
                    Name = "Na wczoraj",
                    ScienceClub = sc[0],
                },
                new CategoryOfTask
                {
                    Name = "W wolnej chwili",
                    ScienceClub = sc[0],
                },
                new CategoryOfTask
                {
                    Name = "Pilne",
                    ScienceClub = sc[1],
                },
                new CategoryOfTask
                {
                    Name = "Na wczoraj",
                    ScienceClub = sc[1],
                },
                new CategoryOfTask
                {
                    Name = "W wolnej chwili",
                    ScienceClub = sc[1],
                },new CategoryOfTask
                {
                    Name = "Pilne",
                    ScienceClub = sc[2],
                },
                new CategoryOfTask
                {
                    Name = "Na wczoraj",
                    ScienceClub = sc[2],
                },
                new CategoryOfTask
                {
                    Name = "W wolnej chwili",
                    ScienceClub = sc[2],
                },
            };
            catsOfTask.ForEach(c => context.CategoriesOfTask.Add(c));
            context.SaveChanges();

            var taskState = new List<TaskState>
            {
                new TaskState
                {
                    Name = "Nowe"
                },
                new TaskState
                {
                    Name = "W trakcie"
                },
                new TaskState
                {
                    Name = "Zakończone"
                },
                new TaskState
                {
                    Name = "Usunięte"
                },
            };
            taskState.ForEach(t => context.TaskStates.Add(t));
            context.SaveChanges();

            var mem = new List<Member>
            {
                new Member{
                    BirthDate = new DateTime(1997,2,14),
                    Email = "kacper.jedrzejewski@int.pl",
                    FirstName = "Kacper",
                    LastName = "Jędrzejewski",
                    Username = "_Ashby",
                    JoinDate = DateTime.Now,
                    FieldOfStudyID = 1,
                    ScienceClubs = new List<ScienceClub>()
                },
            };

            mem[0].ScienceClubs.Add(context.ScienceClubs.Where(s => s.Name.Contains("Genbit")).Single());
            mem.ForEach(m => context.Members.Add(m));
            context.SaveChanges();

            var sc1 = context.ScienceClubs.Find(1);
            sc1.ScienceClub_Member_Roles = new List<ScienceClub_Member_Role>();
            sc1.ScienceClub_Member_Roles.Add(new ScienceClub_Member_Role
            {
                Member = context.Members.Find(1),
                Role = context.Roles.Find(4)
            });
            var sc2 = context.ScienceClubs.Find(2);
            sc2.ScienceClub_Member_Roles = new List<ScienceClub_Member_Role>();
            sc2.ScienceClub_Member_Roles.Add(new ScienceClub_Member_Role
            {
                Member = context.Members.Find(1),
                Role = context.Roles.Find(9)
            });
            var sc3 = context.ScienceClubs.Find(3);
            sc3.ScienceClub_Member_Roles = new List<ScienceClub_Member_Role>();
            sc3.ScienceClub_Member_Roles.Add(new ScienceClub_Member_Role
            {
                Member = context.Members.Find(1),
                Role = context.Roles.Find(14)
            });
            context.SaveChanges();
        }
    }
}