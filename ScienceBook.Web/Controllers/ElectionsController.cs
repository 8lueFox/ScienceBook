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

            foreach (var item in opts)
            {
                if (item.Contains("@"))
                {
                    var tempItem = item.Replace('@', ' ');
                    var stringsInOption = tempItem.Split(' ');
                    var user = db.ScienceClubs.Where(sc => sc.ID == ScienceClubID)
                                              .FirstOrDefault()
                                              .Members.Where(m => m.FirstName.Equals(stringsInOption[1]) && m.LastName.Equals(stringsInOption[2]))
                                              .FirstOrDefault();
                    if(user == null)
                    {
                        election.OptionsInElection.Add(new OptionsInElection
                        {
                            Value = item.Remove(0, 1)
                        });
                    }
                    else
                    {
                        election.OptionsInElection.Add(new OptionsInElection
                        {
                            Value = "<a href='/Members/Details/" + user.ID + "'>" + user.FirstName + " " + user.LastName + "</a>"
                        });
                    }
                }
                else
                {
                    election.OptionsInElection.Add(new OptionsInElection
                    {
                        Value = item
                    });
                }
            }

            db.Elections.Add(election);
            db.SaveChanges();

            return Redirect(RedirectURL);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVote(int ElectionID, string[] electionOption)
        {
            db = new ScienceBookContext();
            var election = db.Elections.Find(ElectionID);
            foreach (var item in election.OptionsInElection)
            {
                foreach (var opt in electionOption)
                {
                    if (item.Value.Equals(opt))
                    {
                        item.Votes.Add(new Vote
                        {
                            Member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault()
                        });
                    }
                }
            }
            db.SaveChanges();
            return Redirect("/ScienceClubs/Details/" + election.ScienceClubID);
        }
    }
}