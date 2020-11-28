using ScienceBook.Web.DAL;
using ScienceBook.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private ScienceBookContext db;

        public SearchController()
        {
            db = new ScienceBookContext();
        }

        public ActionResult Search(string searchString)
        {
            ViewBag.SearchString = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                var list = GetScienceClubs(searchString);
                var secondlist = GetMembers(searchString);
                secondlist.ForEach(s => list.Add(s));
                return View(list);
            }
            return View(new List<SearchingResult>());
        }

        public List<SearchingResult> GetScienceClubs(string phrase)
        {
            var list = db.ScienceClubs
                         .Where(s => s.Name.ToLower().Contains(phrase.ToLower()))
                         .Select(s => new { Id = s.ID, Name = s.Name, Logo = s.Logo })
                         .ToList()
                         .Select(x => new SearchingResult
                         {
                             Id = x.Id,
                             Name = x.Name,
                             Logo = x.Logo,
                             Type = "ScienceClubs"
                         }).ToList();
            return list;
        }

        public List<SearchingResult> GetMembers(string phrase)
        {
            var list = db.Members
                         .Where(m => m.Username.ToLower().Contains(phrase.ToLower())
                         || (m.FirstName + " " + m.LastName).ToLower().Contains(phrase.ToLower()))
                         .Select(m => new { Id = m.ID, Name = (m.FirstName + " " + m.LastName), Logo = m.Logo })
                         .ToList()
                         .Select(x => new SearchingResult
                         {
                             Id = x.Id,
                             Name = x.Name,
                             Logo = x.Logo,
                             Type = "Members"
                         }).ToList();
            return list;
        }
    }
}