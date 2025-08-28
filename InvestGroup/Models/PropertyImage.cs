using System.ComponentModel.DataAnnotations;

namespace InvestGroup.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
