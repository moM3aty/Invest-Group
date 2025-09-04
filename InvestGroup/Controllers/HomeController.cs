using InvestGroup.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InvestGroup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _context.Properties.ToListAsync(); 
            ViewData["FinishingProperties"] = await _context.Properties.ToListAsync();
            return View(properties);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var property = await _context.Properties.Include(p => p.GalleryImages).FirstOrDefaultAsync(m => m.Id == id);
            if (property == null) return NotFound();
            return View(property);
        }

        public async Task<IActionResult> Furniture()
        {
            var furnitureCategory = await _context.GalleryCategories.Include(g => g.Images).FirstOrDefaultAsync(g => g.NameEn == "Furniture");
            if (furnitureCategory == null) return NotFound();
            return View(furnitureCategory);
        }

        public async Task<IActionResult> Decore()
        {
            var finishingCategory = await _context.GalleryCategories.Include(g => g.Images).FirstOrDefaultAsync(g => g.NameEn == "Decorating & Finishing");
            if (finishingCategory == null) return NotFound();
            return View(finishingCategory);
        }
    }
}
