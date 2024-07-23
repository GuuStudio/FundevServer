using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Data
{
    [Table("Categories")]
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<Product>? Products { get; set; }
    }
}
