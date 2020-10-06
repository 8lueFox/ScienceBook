using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("TaskStates")]
    public class TaskState
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}