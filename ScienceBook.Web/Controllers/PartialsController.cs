using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    public class PartialsController : Controller
    {
        ScienceBookContext db = new ScienceBookContext();
        // GET: Partials
        public ActionResult GetUserInfo()
        {
            var member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.Username = member.FirstName + member.LastName;
            ViewBag.Member = member;
            return PartialView("_UserInfo");
        }

        public ActionResult GetUserScienceClubs()
        {
            var scienceClubs = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault().ScienceClubs;
            var images = new List<string>();
            foreach (var item in scienceClubs)
            {
                images.Add(Imager.ByteArrayToStringImage(item.Logo));
            }
            ViewBag.ScienceClubs = scienceClubs;
            ViewBag.Images = images;
            return PartialView("_UserScienceClubs");
        }
    }
}