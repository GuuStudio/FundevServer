using FundevServer.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Models
{
    public class CartItemModel
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; } = null!;
        public string StoreId { get; set; } = null!;
        public DateTime DateAdded { get; set; }

        // Navigation property (nếu bạn sử dụng Entity Framework)
        [ForeignKey(nameof(ProductId))]
        public virtual ProductModel Product { get; set; } = null!;

    }
}
