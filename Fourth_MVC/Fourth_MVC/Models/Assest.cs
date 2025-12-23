using System.ComponentModel.DataAnnotations;

namespace Fourth_MVC.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Area { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;
        // قيم ممكنة: "Land", "Aqarat", "Complex", "Field"
    }
}
