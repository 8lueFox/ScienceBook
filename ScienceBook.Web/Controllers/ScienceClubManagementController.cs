using ScienceBook.Web.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    public class ScienceClubManagementController : Controller
    {
        private ScienceBookContext db = new ScienceBookContext();

        public ActionResult Members(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sc = db.ScienceClubs.Find(id);
            var membersRole = new List<string>();
            foreach (var item in sc.Members)
            {
                membersRole.Add(sc.ScienceClub_Member_Roles.Where(smr => smr.MemberID == item.ID).First().Role.Name);
            }
            ViewBag.Members = sc.Members.ToList();
            ViewBag.MembersRole = membersRole;
            ViewBag.Roles = db.Roles.Where(r => r.ScienceClubID == id).ToList();

            return PartialView("_Members");
        }

        public ActionResult General(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("_General");
        }
    }
}