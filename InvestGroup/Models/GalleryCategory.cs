using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestGroup.Models
{
    public class GalleryCategory
    {
        public int Id { get; set; }

        [Display(Name = "Name (English)")]
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Display(Name = "Name (Arabic)")]
        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        [Display(Name = "Description (English)")]
        [Required]
        public string DescriptionEn { get; set; }

        [Display(Name = "Description (Arabic)")]
        [Required]
        public string DescriptionAr { get; set; }

        public virtual ICollection<GalleryImage> Images { get; set; } = new List<GalleryImage>();
    }
}
