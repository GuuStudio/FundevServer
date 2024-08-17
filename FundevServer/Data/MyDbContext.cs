using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FundevServer.Models;

namespace FundevServer.Data
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Store)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u =>u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(od => od.Customer)
                .WithMany(u => u.Ordering)
                .HasForeignKey(od => od.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                .HasOne(od => od.Store)
                .WithMany(u => u.Orders)
                .HasForeignKey(od => od.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
