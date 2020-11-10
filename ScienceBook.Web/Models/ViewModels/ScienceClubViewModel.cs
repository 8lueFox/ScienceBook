using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.ViewModels
{
    public class ScienceClubViewModel
    {
        public ScienceClub ScienceClub { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Election> Elections { get; set; }
        public Event Event { get; set; }
    }
}