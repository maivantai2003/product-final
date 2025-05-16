using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuanLySanPhamBasic.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<BookCatalog> BookCatalogs { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<TodoItem> TodoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookCatalog>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.CatalogId });

                entity.HasOne(e => e.Book)
                    .WithMany(e => e.BookCatalogs)
                    .HasForeignKey(e => e.BookId);

                entity.HasOne(e => e.Catalog)
                    .WithMany(e => e.BookCatalogs)
                    .HasForeignKey(e => e.CatalogId);
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.BookId });

                entity.HasOne(e => e.Cart)
                    .WithMany(e => e.CartDetails)
                    .HasForeignKey(e => e.CartId);

                entity.HasOne(e => e.Book)
                    .WithMany(e => e.CartDetails)
                    .HasForeignKey(e => e.BookId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.Book)
                    .WithMany()
                    .HasForeignKey(e => e.BookId);
            });
        }
    }
}
