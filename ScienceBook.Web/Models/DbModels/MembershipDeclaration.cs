using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBook.Web.Models.DbModels
{
    public class MembershipDeclaration
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public int MemberID { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DayOfCreate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateOfConsideration { get; set; }
        public string Interests { get; set; }
        public string SignerEmail { get; set; }
        public bool IsSigned { get; set; }

        public virtual Member Member { get; set; }
        public virtual ScienceClub ScienceClub { get; set; }
    }
}