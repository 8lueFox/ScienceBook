using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class FieldOfStudy
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<Member> Members { get; set; }
    }
}