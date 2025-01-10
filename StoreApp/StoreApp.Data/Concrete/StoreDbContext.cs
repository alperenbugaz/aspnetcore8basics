using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategory>();

            modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryUrl)
            .IsUnique();

            modelBuilder.Entity<Product>().HasData(
            


            new List<Product>()
            {
                new() {ProductId = 1, Name = "Samsung S24", Description = "Samsung Türkiye Garantili",Price = 50000 },
                new() {ProductId = 2, Name = "Samsung S25", Description = "Samsung Türkiye Garantili",Price = 60000 },
                new() {ProductId = 3, Name = "Samsung S26", Description = "Samsung Türkiye Garantili",Price = 70000 },
                new() {ProductId = 4, Name = "Samsung S27", Description = "Samsung Türkiye Garantili",Price = 80000 },
                new() {ProductId = 5, Name = "Samsung S28", Description = "Samsung Türkiye Garantili",Price = 90000 },
                new() {ProductId = 6, Name = "Samsung S29", Description = "Samsung Türkiye Garantili",Price = 100000 },
                new() {ProductId = 7, Name = "Arçelik Çamaşır Makinesi", Description = "Güzel Makine",Price = 110000 },
            }
            );

            modelBuilder.Entity<Category>().HasData(
                new List<Category>() 
                {
                    new() {CategoryId = 1, CategoryName = "Telefon", CategoryUrl = "telefon"},
                    new() {CategoryId = 2, CategoryName = "Bilgisayar", CategoryUrl = "bilgisayar"},
                    new() {CategoryId = 3, CategoryName = "Elektronik", CategoryUrl = "elektronik"},
                    new() {CategoryId = 4, CategoryName = "Beyaz Eşya", CategoryUrl = "beyaz-esya"},
                    new() {CategoryId = 5, CategoryName = "Mobilya", CategoryUrl = "mobilya"},
                    new() {CategoryId = 6, CategoryName = "Kırtasiye", CategoryUrl = "kirtasiye"},
                }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new List<ProductCategory>()
                {
                    new() {ProductId = 1, CategoryId = 1},
                    new() {ProductId = 2, CategoryId = 1},
                    new() {ProductId = 3, CategoryId = 1},
                    new() {ProductId = 4, CategoryId = 1},
                    new() {ProductId = 5, CategoryId = 1},
                    new() {ProductId = 6, CategoryId = 1},
                    new() {ProductId = 7, CategoryId = 4},
                }
            );
        }   
    }
}