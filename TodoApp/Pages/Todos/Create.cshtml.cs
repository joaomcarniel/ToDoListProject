using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages.Todos
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public TodoItem TodoItem { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            TodoItem.CreatedAt = DateTime.UtcNow;
            _db.TodoItems.Add(TodoItem);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
