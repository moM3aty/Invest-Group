using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestGroup.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category.")]
        public int Category { get; set; }

        [Display(Name = "Title (English)")]
        [Required(ErrorMessage = "English title is required.")]
        [StringLength(100)]
        public string TitleEn { get; set; }

        [Display(Name = "Title (Arabic)")]
        [Required(ErrorMessage = "Arabic title is required.")]
        [StringLength(100)]
        public string TitleAr { get; set; }

        [Display(Name = "Location (English)")]
        [StringLength(100)]
        public string? LocationEn { get; set; }

        [Display(Name = "Location (Arabic)")]
        [StringLength(100)]
        public string? LocationAr { get; set; }

        [Display(Name = "Overview (English)")]
        public string? OverviewEn { get; set; }

        [Display(Name = "Overview (Arabic)")]
        public string? OverviewAr { get; set; }

        [Display(Name = "Detailed Description (English)")]
        public string? DescriptionEn { get; set; }

        [Display(Name = "Detailed Description (Arabic)")]
        public string? DescriptionAr { get; set; }

        [Display(Name = "Features (English) - Separate with '|'")]
        public string? FeaturesEn { get; set; }

        [Display(Name = "Features (Arabic) - Separate with '|'")]
        public string? FeaturesAr { get; set; }

        [Display(Name = "Unit Types (English) - Separate with '|'")]
        public string? UnitTypesEn { get; set; }

        [Display(Name = "Unit Types (Arabic) - Separate with '|'")]
        public string? UnitTypesAr { get; set; }

        [Display(Name = "Project Area (English)")]
        public string? ProjectAreaEn { get; set; }

        [Display(Name = "Project Area (Arabic)")]
        public string? ProjectAreaAr { get; set; }

        [Display(Name = "Delivery Info (English)")]
        public string? DeliveryInfoEn { get; set; }

        [Display(Name = "Delivery Info (Arabic)")]
        public string? DeliveryInfoAr { get; set; }

        [Display(Name = "Maintenance Info (English)")]
        public string? MaintenanceInfoEn { get; set; }

        [Display(Name = "Maintenance Info (Arabic)")]
        public string? MaintenanceInfoAr { get; set; }

        [Display(Name = "Special Offer (English)")]
        public string? SpecialOfferEn { get; set; }

        [Display(Name = "Special Offer (Arabic)")]
        public string? SpecialOfferAr { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Payment Plan (English) - Separate with '|'")]
        public string? PaymentPlanEn { get; set; }

        [Display(Name = "Payment Plan (Arabic) - Separate with '|'")]
        public string? PaymentPlanAr { get; set; }

        [Display(Name = "Main Image")]
        public string MainImageUrl { get; set; }

        public virtual ICollection<PropertyImage> GalleryImages { get; set; } = new List<PropertyImage>();
    }
}
