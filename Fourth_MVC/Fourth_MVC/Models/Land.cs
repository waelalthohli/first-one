using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fourth_MVC.Models
{
    
        [Table("Lands")]
        public class Land
        {
            public int Id { get; set; }

            [Required, MaxLength(50)]
            public string Name { get; set; } = string.Empty;

            [Required, MaxLength(200)]
            public string Location { get; set; } = string.Empty;

            [Required, MaxLength(200)]
            public string Area { get; set; } = string.Empty;

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;
        }
    
}
