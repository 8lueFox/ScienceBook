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
    public class PostsController : Controller
    {
        ScienceBookContext db;

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(int ScienceClubID, string Title, string Text, string LogoBase64)
        {
            db = new ScienceBookContext();

            Post post = new Post
            {
                Member = db.Members.Where(m => m.Email.Equals(User.Identity.Name)).FirstOrDefault(),
                ScienceClub = db.ScienceClubs.Find(ScienceClubID),
                PublicationDay = DateTime.Now,
                Title = Title,
                Text = Text,
                Img = LogoBase64
            };

            db.Posts.Add(post);
            db.SaveChanges();
            return Redirect("/ScienceClubs/Details/" + ScienceClubID);
        }
    }
}