using FundevServer.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FundevServer.Models
{
    public class AddProductModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required] 
        public int Quantity { get; set; }
        public IFormFile FileImage { get; set; } = null!;
        public int cateId { get; set; }
        public string userId { get; set; } = Guid.NewGuid().ToString();

    }
}
