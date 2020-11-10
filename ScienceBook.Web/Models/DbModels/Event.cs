using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Events")]
    public class Event
    {
        public int ID { get; set; }
        public int MemberID { get; set; } // użytkownik który dodał
        public int ScienceClubID { get; set; } // koło które organizuje
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public DateTime PublicationDay { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public virtual Member Member { get; set; }
        public virtual ScienceClub ScienceClub { get; set; }
    }
}