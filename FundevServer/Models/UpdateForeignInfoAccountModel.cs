using System.ComponentModel.DataAnnotations;

namespace FundevServer.Models
{
    public class UpdateForeignInfoAccountModel
    {
        public string Id { get; set; } = string.Empty;
        public string? AddressHome { get; set; }
        [Phone]
        public string? PhoneNumber { get; set;}
    }
}
