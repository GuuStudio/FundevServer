using FundevServer.Data;
using FundevServer.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Models
{
    public class UploadOrderModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string StoreId { get; set; } = null!;
        public string? ShippingAddress { get; set; }

    }
}
