using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundevServer.Data
{
    [Table("UserFollows")]
    public class UserFollow
    {
        [Key]
        public int Id { get; set; }
         //ID của người dùng được follow
        public string StoreId { get; set; } = null!;
        public string FollowerId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ApplicationUser Store { get; set; } = null!;
        public ApplicationUser Follower { get; set; } = null!;
    }
}
