using FundevServer.Data;

namespace FundevServer.Models
{
    public class UserModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public string? UserImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressHome { get; set; }
        public List<ProductModel>? ProductModels { get; set; }
        public ICollection<UserFollow>? Followers { get; set; }
        public ICollection<UserFollow>? Following { get; set; } 
    }
}
