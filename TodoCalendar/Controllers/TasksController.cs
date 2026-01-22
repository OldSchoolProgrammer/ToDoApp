using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoCalendar.Data;
using TodoCalendar.Models;
using TodoCalendar.ViewModels;

namespace TodoCalendar.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string? search, int? categoryId, int? priority, bool? completed)
        {
            var tasksQuery = _context.Tasks.Include(t => t.Category).AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(search))
            {
                tasksQuery = tasksQuery.Where(t => 
                    t.Title.Contains(search) || 
                    (t.Description != null && t.Description.Contains(search)) ||
                    (t.Tags != null && t.Tags.Contains(search)));
            }

            if (categoryId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.CategoryId == categoryId.Value);
            }

            if (priority.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.Priority == priority.Value);
            }

            if (completed.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.IsCompleted == completed.Value);
            }

            var tasks = await tasksQuery
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category != null ? t.Category.Name : null,
                    CategoryColor = t.Category != null ? t.Category.Color : null,
                    Tags = t.Tags,
                    CreatedDate = t.CreatedDate,
                    CompletedDate = t.CompletedDate
                })
                .ToListAsync();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Priority = priority;
            ViewBag.Completed = completed;

            return View(tasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                IsCompleted = task.IsCompleted,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name,
                CategoryColor = task.Category?.Color,
                Tags = task.Tags,
                CreatedDate = task.CreatedDate,
                CompletedDate = task.CompletedDate
            };

            return View(viewModel);
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new TaskCreateViewModel
            {
                AvailableCategories = await _context.Categories.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new TodoTask
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    DueDate = viewModel.DueDate,
                    Priority = viewModel.Priority,
                    CategoryId = viewModel.CategoryId,
                    Tags = viewModel.Tags,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false
                };

                _context.Add(task);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Task created successfully!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.AvailableCategories = await _context.Categories.ToListAsync();
            return View(viewModel);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new TaskEditViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                IsCompleted = task.IsCompleted,
                CategoryId = task.CategoryId,
                Tags = task.Tags,
                AvailableCategories = await _context.Categories.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var task = await _context.Tasks.FindAsync(id);
                    if (task == null)
                    {
                        return NotFound();
                    }

                    task.Title = viewModel.Title;
                    task.Description = viewModel.Description;
                    task.DueDate = viewModel.DueDate;
                    task.Priority = viewModel.Priority;
                    task.CategoryId = viewModel.CategoryId;
                    task.Tags = viewModel.Tags;

                    // Handle completion status change
                    if (viewModel.IsCompleted && !task.IsCompleted)
                    {
                        task.CompletedDate = DateTime.Now;
                    }
                    else if (!viewModel.IsCompleted && task.IsCompleted)
                    {
                        task.CompletedDate = null;
                    }
                    task.IsCompleted = viewModel.IsCompleted;

                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "Task updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.AvailableCategories = await _context.Categories.ToListAsync();
            return View(viewModel);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Task deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/ToggleComplete
        [HttpPost]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            task.IsCompleted = !task.IsCompleted;
            task.CompletedDate = task.IsCompleted ? DateTime.Now : null;

            await _context.SaveChangesAsync();

            return Json(new { success = true, isCompleted = task.IsCompleted });
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
