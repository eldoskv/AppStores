using AppStores.Data;
using AppStores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppStores.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,,Discription,Image,Category")] Applications application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }
        public async Task<IActionResult> Index(string searchString, string category)
        {
            var applications = from g in _context.Applications
                               select g;

            if (!string.IsNullOrEmpty(searchString))
            {
                applications = applications.Where(g => g.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category) && category != "1")
            {
                applications = applications.Where(g => g.Category == category);
            }

            ViewBag.Categories = new[]
            {
                new { Id = "1", Name = "СоцСети" },
                new { Id = "2", Name = "Спрт" },
                new { Id = "3", Name = "Еда" },
                new { Id = "4", Name = "Транспорт" },
            };

            return View(await applications.ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
