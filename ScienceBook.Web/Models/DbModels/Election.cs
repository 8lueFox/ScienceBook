using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Elections")]
    public class Election
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public string Name { get; set; }
        public DateTime DayOfStart { get; set; }
        public DateTime DayOfEnd { get; set; }
        public int CountOfChoices { get; set; }

        public virtual ScienceClub ScienceClub { get; set; }
        public virtual List<OptionsInElection> OptionsInElection { get; set; }
    }
}