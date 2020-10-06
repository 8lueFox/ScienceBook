using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Departments")]
    public class Department
    {
        public int ID { get; set; }
        public int UniversityID { get; set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }

        public virtual University University { get; set; }
        public virtual List<ScienceClub> ScienceClubs { get; set; }
    }
}