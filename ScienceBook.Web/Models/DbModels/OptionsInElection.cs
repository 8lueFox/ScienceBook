using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("OptionsInElections")]
    public class OptionsInElection
    {
        public int ID { get; set; }
        public int ElectionID { get; set; }
        public string value { get; set; }

        public virtual Election Election { get; set; }
    }
}