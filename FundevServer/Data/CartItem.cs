using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Data
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; } = null!;
        public string StoreId { get; set; } = null!;
        public DateTime DateAdded { get; set; }

        // Navigation property (nếu bạn sử dụng Entity Framework)
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;


    }
}
