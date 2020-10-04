using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            ViewBag.Logo = Imager.ByteArrayToStringImage(scienceClub.Logo);
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
            db.SaveChanges();

            return RedirectToAction("Details", "ScienceClubs", new { id = id});
        }

        // GET: ScienceClubs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryOfScienceClubID = new SelectList(db.CategoriesOfScienceClub, "ID", "Name");
            return View();
        }

        // POST: ScienceClubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryOfScienceClubID,Name,Description,CreationDate,Logo")] ScienceClub scienceClub)
        {
            if (ModelState.IsValid)
            {
                db.ScienceClubs.Add(scienceClub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryOfScienceClubID = new SelectList(db.CategoriesOfScienceClub, "ID", "Name", scienceClub.CategoryOfScienceClubID);
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
