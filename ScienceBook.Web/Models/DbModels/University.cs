using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class University
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<Department> Departments { get; set; }
        public virtual List<ScienceClub> ScienceClubs { get; set; }
    }
}