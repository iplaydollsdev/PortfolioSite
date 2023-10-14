using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreLibrary.DataAccess
{
   public class AppDbContext : IdentityDbContext
   {
      public AppDbContext()
      {
      }
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


      public DbSet<CategoryModel> Category { get; set; }
      public DbSet<ProductModel> Product { get; set; }
      public DbSet<CartModel> Cart { get; set; }
      public DbSet<OrderModel> Order { get; set; }
      public DbSet<OrderProductModel> OrderProduct { get; set; }
      public DbSet<UserModel> User { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<CategoryModel>()
             .ToTable("categories")
             .HasKey(c => c.Id);

         modelBuilder.Entity<CartModel>()
            .ToTable("carts")
            .HasKey(c => c.Id);

         modelBuilder.Entity<OrderModel>()
            .ToTable("orders")
            .HasKey(o => o.Id);

         modelBuilder.Entity<ProductModel>()
             .ToTable("products")
             .HasKey(p => p.Id);
      }
   }
}

