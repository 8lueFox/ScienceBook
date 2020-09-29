using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class Member
    {
        public int ID { get; set; }
        //public int FieldOfStudyID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public byte[] Logo { get; set; }

        //public virtual FieldOfStudy FieldOfStudy { get; set; }
        //public virtual List<ScienceClub> ScienceClubs { get; set; }
        //public virtual List<Task> Tasks { get; set; }
    }
}