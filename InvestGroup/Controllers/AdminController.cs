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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TotalProperties"] = await _context.Properties.CountAsync();
            ViewData["TotalApartments"] = await _context.Properties.CountAsync(p => p.Category == 0);
            ViewData["TotalFinishings"] = await _context.Properties.CountAsync(p => p.Category == 1);
            ViewData["RecentProperties"] = await _context.Properties.OrderByDescending(p => p.Id).Take(5).ToListAsync();
            ViewData["TotalGalleryImages"] = await _context.GalleryImages.CountAsync();
            return View();
        }

        public async Task<IActionResult> Properties()
        {
            var properties = await _context.Properties.OrderByDescending(p => p.Id).ToListAsync();
            return View(properties);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var property = await _context.Properties.Include(p => p.GalleryImages).FirstOrDefaultAsync(p => p.Id == id);
            if (property == null) return NotFound();
            return View(property);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property property, IFormFile mainImageFile, List<IFormFile> galleryFiles)
        {
            ModelState.Remove("MainImageUrl");

            if (ModelState.IsValid)
            {
                if (mainImageFile != null)
                {
                    property.MainImageUrl = await UploadImage(mainImageFile, "properties");
                }
                else
                {
                    ModelState.AddModelError("MainImageUrl", "Main image is required.");
                    return View(property);
                }

                _context.Add(property);
                await _context.SaveChangesAsync(); // Save property first to get an ID

                if (galleryFiles != null && galleryFiles.Count > 0)
                {
                    foreach (var file in galleryFiles)
                    {
                        var imageUrl = await UploadImage(file, "properties");
                        // Add image to the property's collection
                        property.GalleryImages.Add(new PropertyImage { ImageUrl = imageUrl });
                    }
                    // No need to save again if change tracking is doing its job, but explicit is safer.
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Properties));
            }
            return View(property);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var property = await _context.Properties.Include(p => p.GalleryImages).FirstOrDefaultAsync(p => p.Id == id);
            if (property == null) return NotFound();
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Property property, IFormFile mainImageFile, List<IFormFile> galleryFiles)
        {
            if (id != property.Id) return NotFound();

            var propertyToUpdate = await _context.Properties.Include(p => p.GalleryImages).FirstOrDefaultAsync(p => p.Id == id);
            if (propertyToUpdate == null) return NotFound();
            ModelState.Remove("MainImageUrl");
            ModelState.Remove("mainImageFile");
            if (ModelState.IsValid)
            {
                propertyToUpdate.TitleEn = property.TitleEn;
                propertyToUpdate.TitleAr = property.TitleAr;
                propertyToUpdate.LocationEn = property.LocationEn;
                propertyToUpdate.LocationAr = property.LocationAr;
                propertyToUpdate.OverviewEn = property.OverviewEn;
                propertyToUpdate.OverviewAr = property.OverviewAr;
                propertyToUpdate.DescriptionEn = property.DescriptionEn;
                propertyToUpdate.DescriptionAr = property.DescriptionAr;
                propertyToUpdate.FeaturesEn = property.FeaturesEn;
                propertyToUpdate.FeaturesAr = property.FeaturesAr;
                propertyToUpdate.UnitTypesEn = property.UnitTypesEn;
                propertyToUpdate.UnitTypesAr = property.UnitTypesAr;
                propertyToUpdate.PaymentPlanEn = property.PaymentPlanEn;
                propertyToUpdate.PaymentPlanAr = property.PaymentPlanAr;
                propertyToUpdate.ProjectAreaEn = property.ProjectAreaEn;
                propertyToUpdate.ProjectAreaAr = property.ProjectAreaAr;
                propertyToUpdate.DeliveryInfoEn = property.DeliveryInfoEn;
                propertyToUpdate.DeliveryInfoAr = property.DeliveryInfoAr;
                propertyToUpdate.MaintenanceInfoEn = property.MaintenanceInfoEn;
                propertyToUpdate.MaintenanceInfoAr = property.MaintenanceInfoAr;
                propertyToUpdate.SpecialOfferEn = property.SpecialOfferEn;
                propertyToUpdate.SpecialOfferAr = property.SpecialOfferAr;
                propertyToUpdate.Category = property.Category;
                propertyToUpdate.Price = property.Price;

                if (mainImageFile != null)
                {
                    propertyToUpdate.MainImageUrl = await UploadImage(mainImageFile, "properties");
                }

                if (galleryFiles != null && galleryFiles.Count > 0)
                {
                    foreach (var file in galleryFiles)
                    {
                        var imageUrl = await UploadImage(file, "properties");
                        propertyToUpdate.GalleryImages.Add(new PropertyImage { ImageUrl = imageUrl });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Properties));
            }
            return View(propertyToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var property = await _context.Properties.FirstOrDefaultAsync(m => m.Id == id);
            if (property == null) return NotFound();
            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Properties)); // Redirect to properties list
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteGalleryImage(int id)
        {
            var image = await _context.PropertyImages.FindAsync(id);
            if (image != null)
            {
                // Optional: Delete the physical file
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(wwwRootPath, image.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.PropertyImages.Remove(image);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // Overloaded method to handle subfolders
        private async Task<string> UploadImage(IFormFile file, string subfolder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string uploadDir = Path.Combine(wwwRootPath, "uploads", subfolder);
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = $"{fileName}_{System.DateTime.Now:yymmssfff}{extension}";
            string path = Path.Combine(uploadDir, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/uploads/{subfolder}/{fileName}";
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
