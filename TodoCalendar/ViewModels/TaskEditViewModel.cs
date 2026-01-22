using System.ComponentModel.DataAnnotations;
using TodoCalendar.Models;

namespace TodoCalendar.ViewModels
{
    public class TaskEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Priority must be Low (1), Medium (2), or High (3)")]
        public int Priority { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [StringLength(500, ErrorMessage = "Tags cannot exceed 500 characters")]
        public string? Tags { get; set; }

        public List<Category>? AvailableCategories { get; set; }
    }
}
