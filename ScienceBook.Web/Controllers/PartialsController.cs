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
                if (item.Logo != null)
                    images.Add(Imager.ByteArrayToStringImage(item.Logo));
                else
                    images.Add(item.LogoS);
            }
            ViewBag.ScienceClubs = scienceClubs;
            ViewBag.Images = images;
            return PartialView("_UserScienceClubs");
        }

        public ActionResult GetScienceClubMembers(int id)
        {
            var scienceClub = db.ScienceClubs.Find(id);
            var members = scienceClub.Members;
            var logos = new List<string>();

            foreach (var item in members)
            {
                logos.Add(Imager.ByteArrayToStringImage(item.Logo));
            }
            ViewBag.Logos = logos;
            ViewBag.Members = members;

            return PartialView("_ScienceClubMembers");
        }

        public ActionResult UniversityAcademies(string name)
        {
            ViewBag.Name = name;
            if(name != null)
            {
                var acaList = db.Departments.Where(a => a.University.Name.Equals(name)).ToList();
                ViewBag.Academies = acaList;
            }
            return PartialView();
        }
    }
}