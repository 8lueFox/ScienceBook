using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.DbModels;
using ScienceBook.Web.Models.Statics;
using ScienceBook.Web.Models.ViewModels;

namespace ScienceBook.Web.Controllers
{
    public class ScienceClubsController : Controller
    {
        private ScienceBookContext db = new ScienceBookContext();

        // GET: ScienceClubs
        public ActionResult Index()
        {
            var scienceClubs = db.ScienceClubs.Include(s => s.CategoryOfScienceClub);
            return View(scienceClubs.ToList());
        }

        // GET: ScienceClubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScienceClub scienceClub = db.ScienceClubs.Find(id);
            if (scienceClub == null)
            {
                return HttpNotFound();
            }
            if (scienceClub.Members.Contains(db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault()))
                ViewBag.IsJoined = true;
            else
                ViewBag.IsJoined = false;

            if (scienceClub.Logo != null)
                ViewBag.Logo = Imager.ByteArrayToStringImage(scienceClub.Logo);
            else
                ViewBag.Logo = scienceClub.LogoS;

            ScienceClubViewModel view = new ScienceClubViewModel();
            view.ScienceClub = scienceClub;
            var tasks = db.Tasks.Where(t => t.ScienceClubID == scienceClub.ID).ToList();
            var temp = new List<Task>();
            foreach (var item in tasks)
            {
                if(item.EndDay >= DateTime.Now)
                {
                    temp.Add(item);
                }
            }
            tasks = temp;
            tasks = tasks.OrderBy(t => t.StartDay).ToList();
            view.Tasks = tasks;

            var elections = db.Elections.Where(e => e.ScienceClubID == scienceClub.ID && e.DayOfEnd >= DateTime.Now).ToList();
            view.Elections = elections;

            return View(view);
        }

        // GET: ScienceClubs/Join/5
        public ActionResult Join(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.ScienceClubs.Find(id).Members.Add(db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault());

            var scienceClub = db.ScienceClubs.Find(id);
            db.ScienceClubs_Members_Roles.Add(new ScienceClub_Member_Role
            {
                Member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault(),
                Role = scienceClub.Roles.Where(r => r.Name.Equals("Członek")).FirstOrDefault()
            });
            db.SaveChanges();

            return RedirectToAction("Details", "ScienceClubs", new { id = id});
        }

        [Authorize]
        // GET: ScienceClubs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryOfScienceClubID = db.CategoriesOfScienceClub.ToList();
            ViewBag.Universities = db.Universities.ToList();
            return View();
        }

        // POST: ScienceClubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string CategoryOfScienceClubID, string University, string Academy, DateTime CreationDate, string LogoBase64)
        {
            var cat = db.CategoriesOfScienceClub.Where(c => c.Name.Equals(CategoryOfScienceClubID)).FirstOrDefault();
            if (cat == null)
            {
                cat = new CategoryOfScienceClub { Name = CategoryOfScienceClubID };
                db.CategoriesOfScienceClub.Add(cat);
                db.SaveChanges();
            }
            cat = db.CategoriesOfScienceClub.Where(c => c.Name.Equals(CategoryOfScienceClubID)).FirstOrDefault();

            var univ = db.Universities.Where(u => u.Name.Equals(University)).FirstOrDefault();
            if(univ == null)
            {
                univ = new University { Name = University };
                var dep = new Department { Name = Academy, University = univ };
                db.Universities.Add(univ);
                db.Departments.Add(dep);
                db.SaveChanges();
            }

            var department = db.Departments.Where(d => d.Name.Equals(Academy)).FirstOrDefault();
            if(department == null)
            {
                department = new Department { Name = Academy, University = db.Universities.Where(u => u.Name.Equals(University)).FirstOrDefault() };
                db.Departments.Add(department);
                db.SaveChanges();
            }
            department = db.Departments.Where(d => d.Name.Equals(Academy)).FirstOrDefault();

            var mem = User.Identity.Name;
            var member = db.Members.Where(m => m.Email.Equals(mem)).FirstOrDefault();

            var scienceClub = new ScienceClub
            {
                Name = Name, 
                CategoryOfScienceClub = cat,
                CreationDate = CreationDate,
                LogoS = LogoBase64, 
                Department = department, 
                CategoriesOfTasks = new List<CategoryOfTask>
                {
                    new CategoryOfTask{Name = "Pilne"},
                    new CategoryOfTask{Name = "Na wczoraj"},
                    new CategoryOfTask{Name = "W wolnej chwili"}
                },
                Roles = new List<Role>
                {
                    new Role{ Name = "Członek"},
                    new Role{ Name = "Sekretarz"},
                    new Role{ Name = "Vice-przewodniczący"},
                    new Role{ Name = "Przewodniczący"},
                    new Role{ Name = "Opiekun koła"},
                },
                Members = new List<Member>(), 
            };

            scienceClub.Members.Add(db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault());

            if (ModelState.IsValid)
            {
                db.ScienceClubs.Add(scienceClub);
                db.SaveChanges();
                scienceClub = db.ScienceClubs.Where(sc => sc.Name.Equals(scienceClub.Name)).FirstOrDefault();
                scienceClub.ScienceClub_Member_Roles = new List<ScienceClub_Member_Role>();
                db.ScienceClubs_Members_Roles.Add(new ScienceClub_Member_Role
                {
                    Member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault(),
                    Role = scienceClub.Roles.Where(r => r.Name.Equals("Przewodniczący")).FirstOrDefault()
                });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryOfScienceClubID = db.CategoriesOfScienceClub.ToList();
            ViewBag.Universities = db.Universities.ToList();
            return View(scienceClub);
        }

        // GET: ScienceClubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScienceClub scienceClub = db.ScienceClubs.Find(id);
            if (scienceClub == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryOfScienceClubID = new SelectList(db.CategoriesOfScienceClub, "ID", "Name", scienceClub.CategoryOfScienceClubID);
            return View(scienceClub);
        }

        // POST: ScienceClubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryOfScienceClubID,Name,Description,CreationDate,Logo")] ScienceClub scienceClub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scienceClub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryOfScienceClubID = new SelectList(db.CategoriesOfScienceClub, "ID", "Name", scienceClub.CategoryOfScienceClubID);
            return View(scienceClub);
        }

        // GET: ScienceClubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScienceClub scienceClub = db.ScienceClubs.Find(id);
            if (scienceClub == null)
            {
                return HttpNotFound();
            }
            return View(scienceClub);
        }

        // POST: ScienceClubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScienceClub scienceClub = db.ScienceClubs.Find(id);
            db.ScienceClubs.Remove(scienceClub);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
