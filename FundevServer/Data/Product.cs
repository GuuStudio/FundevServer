using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Data
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int cateId { get; set; }
        [ForeignKey("cateId")]
        public Category Category { get; set; } = null!;
        public string userId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("userId")]
        public ApplicationUser User { get; set; } = null!;
    }
}
