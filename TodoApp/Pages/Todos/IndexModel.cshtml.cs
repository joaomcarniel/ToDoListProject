using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<TodoItem> Todos { get; set; } = new List<TodoItem>();

        public async Task OnGetAsync()
        {
            Todos = await _db.TodoItems
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
        }

        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var item = await _db.TodoItems.FindAsync(id);
            if (item == null) return NotFound();
            item.IsDone = !item.IsDone;
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
