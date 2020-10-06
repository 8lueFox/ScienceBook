using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Roles")]
    public class Role
    {
        public int ID { get; set; }
        [Required]
        public int ScienceClubID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ScienceClub ScienceClub { get; set; }
        public virtual List<ScienceClub_Member_Role> ScienceClub_Member_Roles { get; set; }
    }
}