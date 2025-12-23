using System.ComponentModel.DataAnnotations;

namespace Fourth_MVC.Models
{
    public class Aqarat
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Area { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
