using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("CategoriesOfTask")]
    public class CategoryOfTask
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public string Name { get; set; }
        public virtual ScienceClub ScienceClub { get; set; }
    }
}