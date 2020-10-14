using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("OptionsInElections")]
    public class OptionsInElection
    {
        public int ID { get; set; }
        public int ElectionID { get; set; }
        public string Value { get; set; }

        public virtual Election Election { get; set; }
        public virtual List<Vote> Votes { get; set; }

        public OptionsInElection()
        {
            Votes = new List<Vote>();
        }
    }
}