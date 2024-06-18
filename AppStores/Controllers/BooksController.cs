using AppStores.Data;
using AppStores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppStores.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Avtor,Discription,Image,Category")] Books book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        public async Task<IActionResult> Index(string searchString, string category)
        {
            var books = from g in _context.Books
                        select g;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(g => g.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category) && category != "1")
            {
                books = books.Where(g => g.Category == category);
            }

            ViewBag.Categories = new[]
            {
                new { Id = "1", Name = "Романтика" },
                new { Id = "2", Name = "Ужасы" },
                new { Id = "3", Name = "Манга" },
                new { Id = "4", Name = "Фэнтезиы" },
                new { Id = "5", Name = "Детектив" }
            };

            return View(await books.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}