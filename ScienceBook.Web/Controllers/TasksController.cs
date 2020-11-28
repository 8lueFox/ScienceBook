using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ScienceBookContext db;
        // POST: Tasks
        [HttpPost]
        public ActionResult Add(string RedirectURL, int ScienceClubID, string TaskStateID, string Name, string Description, DateTime EndDay, DateTime StartDay, string Member, int CategoryOfTask)
        {
            db = new ScienceBookContext();
            string[] mem = Member.Split(' ');
            string fname = mem[0];
            string lname = mem[1];
            var _member = db.Members.Where(m => m.FirstName.Equals(fname) && m.LastName.Equals(lname)).FirstOrDefault();
            var _taskState = db.TaskStates.FirstOrDefault();
            var _scienceClub = db.ScienceClubs.Find(ScienceClubID);
            var _catOfTask = _scienceClub.CategoriesOfTasks.Where(c => c.ID == CategoryOfTask).FirstOrDefault();

            Task task = new Task
            {
                Name = Name,
                Description = Description,
                EndDay = EndDay,
                StartDay = StartDay,
                CategoryOfTask = _catOfTask,
                ScienceClub = _scienceClub,
                TaskState = _taskState,
                Member = _member
            };

            db.Tasks.Add(task);
            db.SaveChanges();

            return Redirect(RedirectURL);
        }
    }
}