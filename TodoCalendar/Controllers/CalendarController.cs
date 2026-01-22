using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoCalendar.Data;
using TodoCalendar.ViewModels;

namespace TodoCalendar.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calendar
        public IActionResult Index()
        {
            return View();
        }

        // GET: Calendar/GetEvents
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.DueDate.HasValue)
                .ToListAsync();

            var events = tasks.Select(t => new CalendarEventViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Start = t.DueDate,
                Color = GetEventColor(t.Priority, t.Category?.Color),
                AllDay = true,
                Url = Url.Action("Details", "Tasks", new { id = t.Id })
            }).ToList();

            return Json(events);
        }

        private string GetEventColor(int priority, string? categoryColor)
        {
            // Priority-based colors (override category color)
            return priority switch
            {
                3 => "#dc3545", // High - Red
                2 => "#ffc107", // Medium - Yellow
                1 => "#28a745", // Low - Green
                _ => categoryColor ?? "#007bff" // Default - Blue
            };
        }
    }
}
