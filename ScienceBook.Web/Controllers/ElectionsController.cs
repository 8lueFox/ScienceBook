using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    public class ElectionsController : Controller
    {
        private ScienceBookContext db;

        [HttpPost]
        public ActionResult Add(string RedirectURL, int ScienceClubID, string Name, DateTime DayOfStart, DateTime DayOfEnd, int CountOfChoices, string options)
        {
            db = new ScienceBookContext();
            Election election = new Election
            {
                Name = Name,
                ScienceClub = db.ScienceClubs.Find(ScienceClubID),
                DayOfStart = DayOfStart,
                DayOfEnd = DayOfEnd,
                CountOfChoices = CountOfChoices,
                OptionsInElection = new List<OptionsInElection>()
            };

            var opts = options.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

            opts.ForEach(o => election.OptionsInElection.Add(new OptionsInElection
            {
                value = o
            }));

            db.Elections.Add(election);
            db.SaveChanges();

            return Redirect(RedirectURL);
        }
    }
}