using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("ScienceClubs_Members_Roles")]
    public class ScienceClub_Member_Role
    {
        public int ID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [Required]
        public int RoleID { get; set; }
        public virtual Member Member { get; set; }
        public virtual Role Role { get; set; }
    }
}