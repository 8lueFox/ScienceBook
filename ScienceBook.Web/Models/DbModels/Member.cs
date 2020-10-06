using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Members")]
    public class Member
    {
        public int ID { get; set; }
        public int FieldOfStudyID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime JoinDate { get; set; }
        public byte[] Logo { get; set; }

        public virtual FieldOfStudy FieldOfStudy { get; set; }
        public virtual List<ScienceClub> ScienceClubs { get; set; }
        public virtual List<ScienceClub_Member_Role> ScienceClub_Member_Role { get; set; }
        //public virtual List<Task> Tasks { get; set; }
    }
}