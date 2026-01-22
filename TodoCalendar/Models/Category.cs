using System.ComponentModel.DataAnnotations;

namespace TodoCalendar.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(7)]
        public string Color { get; set; } = "#007bff";

        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
    }
}
