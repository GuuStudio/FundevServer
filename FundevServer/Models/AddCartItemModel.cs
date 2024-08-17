using FundevServer.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Models
{
    public class AddCartItemModel
    {
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; } = null!;
        public string StoreId { get; set; } = null!;
    }
}
