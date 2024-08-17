using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FundevServer.Helpers;
using FundevServer.Models;

namespace FundevServer.Data
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PhoneNumber { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string StoreId { get; set; } = null!;

        [Required]
        public string? Status { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public string? ShippingAddress { get; set; }

        public DateTime? ShippingDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? CanceledDate { get; set; }


        public ProductModel Product { get; set; } = null!;
        public UserModel Store { get; set; } = null!;
        public UserModel Customer { get; set; } = null!;


    }
}
