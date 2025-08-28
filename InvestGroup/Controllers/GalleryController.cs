using InvestGroup.Data;
using InvestGroup.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InvestGroup.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GalleryController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Gallery
        public async Task<IActionResult> Index()
        {
            await SeedInitialCategories();
            return View(await _context.GalleryCategories.ToListAsync());
        }

        // GET: Gallery/Manage/5
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryCategory = await _context.GalleryCategories
                .Include(g => g.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (galleryCategory == null)
            {
                return NotFound();
            }

            return View(galleryCategory);
        }

        // POST: Gallery/AddImages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(int categoryId, List<IFormFile> newImages)
        {
            if (newImages != null && newImages.Count > 0)
            {
                foreach (var file in newImages)
                {
                    var imageUrl = await UploadImage(file);
                    var galleryImage = new GalleryImage
                    {
                        ImageUrl = imageUrl,
                        GalleryCategoryId = categoryId
                    };
                    _context.GalleryImages.Add(galleryImage);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Manage), new { id = categoryId });
        }

        // POST: Gallery/DeleteImage/5
        [HttpPost]
        [ValidateAntiForgeryToken] // <-- This was missing
        public async Task<JsonResult> DeleteImage(int id)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(wwwRootPath, image.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.GalleryImages.Remove(image);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string uploadsDir = Path.Combine(wwwRootPath, "uploads", "gallery");
            if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = $"{fileName}_{System.DateTime.Now:yymmssfff}{extension}";
            string path = Path.Combine(uploadsDir, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/uploads/gallery/{fileName}";
        }

        private async Task SeedInitialCategories()
        {
            if (!_context.GalleryCategories.Any(g => g.NameEn == "Furniture"))
            {
                _context.GalleryCategories.Add(new GalleryCategory { NameEn = "Furniture", NameAr = "أثاث منزلي", DescriptionEn = "Description for furniture.", DescriptionAr = "وصف قسم الأثاث." });
            }
            if (!_context.GalleryCategories.Any(g => g.NameEn == "Decorating & Finishing"))
            {
                _context.GalleryCategories.Add(new GalleryCategory { NameEn = "Decorating & Finishing", NameAr = "تشطيبات وديكور", DescriptionEn = "Description for finishing.", DescriptionAr = "وصف قسم التشطيبات." });
            }
            await _context.SaveChangesAsync();
        }
    }
}
