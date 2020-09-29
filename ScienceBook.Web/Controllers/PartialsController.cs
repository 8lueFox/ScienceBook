using ScienceBook.Web.DAL;
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
    }
}