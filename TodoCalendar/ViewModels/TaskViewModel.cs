using System.ComponentModel.DataAnnotations;

namespace TodoCalendar.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryColor { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        
        public string PriorityText => Priority switch
        {
            1 => "Low",
            2 => "Medium",
            3 => "High",
            _ => "Unknown"
        };

        public string PriorityBadgeClass => Priority switch
        {
            1 => "badge bg-success",
            2 => "badge bg-warning",
            3 => "badge bg-danger",
            _ => "badge bg-secondary"
        };
    }
}
