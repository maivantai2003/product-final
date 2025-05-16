using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoList.ToListAsync());
        }

        [HttpPost]
        [Route("add-task")]
        public async Task<IActionResult> Add([FromForm(Name = "description")] string description)
        {
            if (!string.IsNullOrEmpty(description) || !string.IsNullOrWhiteSpace(description))
            {
                var todo = new TodoItem();
                todo.Description = description;
                todo.IsDone = false;
                _context.TodoList.Add(todo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("edit-view/{id}")]
        public IActionResult EditView(int id)
        {
            var todo = _context.TodoList.Find(id);
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                var existingTodo = await _context.TodoList.FindAsync(todoItem.Id);
                if (existingTodo != null)
                {
                    existingTodo.Description = todoItem.Description;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(nameof(EditView), todoItem);
        }

        [HttpDelete]
        [Route("/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTodo = await _context.TodoList.FindAsync(id);
            if (existingTodo != null)
            {
                _context.TodoList.Remove(existingTodo);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/check-done/{id}")]
        public async Task<IActionResult> CheckDone(int id)
        {
            var existingTodo = await _context.TodoList.FindAsync(id);
            if (existingTodo != null)
            {
                existingTodo.IsDone = !existingTodo.IsDone;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
