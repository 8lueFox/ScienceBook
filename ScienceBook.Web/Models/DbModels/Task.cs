using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class Task
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public int CategoryOfTaskID { get; set; }
        public int TaskStateID { get; set; }
        public int MemberID { get; set; }
        public string Name { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Description { get; set; }

        public virtual ScienceClub ScienceClub { get; set; }
        public virtual CategoryOfTask CategoryOfTask { get; set; }
        public virtual TaskState TaskState { get; set; }
        public virtual Member Member { get; set; }
    }
}