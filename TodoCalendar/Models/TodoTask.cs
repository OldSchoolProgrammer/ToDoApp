using System.ComponentModel.DataAnnotations;

namespace TodoCalendar.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Range(1, 3)]
        public int Priority { get; set; } = 2; // 1=Low, 2=Medium, 3=High

        public bool IsCompleted { get; set; } = false;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [StringLength(500)]
        public string? Tags { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? CompletedDate { get; set; }
    }
}
