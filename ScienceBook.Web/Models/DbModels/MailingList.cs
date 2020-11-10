using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class MailingList
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Firm { get; set; }

        public virtual ScienceClub ScienceClub { get; set; }
    }
}