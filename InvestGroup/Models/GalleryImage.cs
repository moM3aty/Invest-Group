using System.ComponentModel.DataAnnotations;

namespace InvestGroup.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int GalleryCategoryId { get; set; }
        public virtual GalleryCategory GalleryCategory { get; set; }
    }
}
