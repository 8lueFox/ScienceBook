using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.DbModels;
using ScienceBook.Web.Models.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        ScienceBookContext db = new ScienceBookContext();

        // GET: AddEvent
        public ActionResult AddEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scienceClub = db.ScienceClubs.Find(id);

            if (scienceClub.Logo != null)
                ViewBag.Logo = Imager.ByteArrayToStringImage(scienceClub.Logo);
            else if (!scienceClub.LogoS.Equals(""))
                ViewBag.Logo = scienceClub.LogoS;
            else
                ViewBag.Logo = $"https://avatars.dicebear.com/api/jdenticon/{scienceClub.Name}.svg";

            return View(scienceClub);
        }

        [HttpPost]
        // POST: AddEvent
        public ActionResult AddEvt(int ScienceClubID, string Title, string Description, string Link, DateTime StartDay, DateTime EndDay, string LogoBase24)
        {
            var scienceClub = db.ScienceClubs.Find(ScienceClubID);

            scienceClub.Events.Add(new Event
            {
                Name = Title, 
                Description = Description,
                Link = Link,
                StartDay = StartDay, 
                EndDay = EndDay, 
                PublicationDay = DateTime.Now,
                Member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault(),
                Img = LogoBase24
            });
            db.SaveChanges();

            return Redirect("/ScienceClubs/Details?id=" + ScienceClubID);
        }

        public ActionResult ShowAll(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scienceClub = db.ScienceClubs.Find(id);

            if (scienceClub.Logo != null)
                ViewBag.Logo = Imager.ByteArrayToStringImage(scienceClub.Logo);
            else if (!scienceClub.LogoS.Equals(""))
                ViewBag.Logo = scienceClub.LogoS;
            else
                ViewBag.Logo = $"https://avatars.dicebear.com/api/jdenticon/{scienceClub.Name}.svg";

            return View(scienceClub);
        }

        public ActionResult InviteGuests(int eventID, List<string> quests)
        {
            var evnts = db.Events.Find(eventID);
            foreach (var item in quests)
            {
                var quest = db.MailingList.Find(int.Parse(item));
                EmailSender.Send(quest.Email, $"Zaproszenia na wydarzenie - {evnts.Name}", "Koło zaprasza Cię na wydarzenie");
            }
            return View();
        }

    }
}