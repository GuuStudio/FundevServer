using FundevServer.Data;

namespace FundevServer.Models
{
    public class FollowModel
    {
        public int Id { get; set; }
        //ID của người dùng được follow
        public string StoreId { get; set; } = null!;
        public string FollowerId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public UserModel Store { get; set; } = null!;
        public UserModel Follower { get; set; } = null!;
    }
}
