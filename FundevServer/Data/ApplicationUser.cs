using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Data
{
    
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? UserImageUrl { get; set; }
        [MaxLength(200)]
        public string? AddressHome { get; set; }

        public virtual ICollection<UserFollow>? Followers { get; set; } // số người theo dõi mình (userId là mình, FollowerId là người ta )
        public virtual ICollection<UserFollow>? Following { get; set; } // Số người mình đang theo dõi 

        public List<Order>? Orders { get; set; }
        public List<Order>? Ordering { get; set; }
    }
}
