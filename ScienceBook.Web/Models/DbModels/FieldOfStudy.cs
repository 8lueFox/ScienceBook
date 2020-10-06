using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("FieldsOfStudy")]
    public class FieldOfStudy
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<Member> Members { get; set; }
    }
}