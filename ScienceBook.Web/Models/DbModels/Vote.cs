using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Votes")]
    public class Vote
    {
        public int ID { get; set; }
        public int OptionsInElectionID { get; set; }
        public int MemberID { get; set; }
        public virtual Member Member { get; set; }
        public virtual OptionsInElection OptionsInElection { get; set; }
    }
}