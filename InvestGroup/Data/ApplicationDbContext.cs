using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InvestGroup.Models;

namespace InvestGroup.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<GalleryCategory> GalleryCategories { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
    }
}
