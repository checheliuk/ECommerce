using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ECommerce.Domain.Model;

namespace ECommerce.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TypeProduct> TypeProduct { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }
        public DbSet<ProductShipping> ProductShippings { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<AddressArea> AddressAreas { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Color> Colors { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
               .HasMany<Category>(s => s.Categories)
               .WithMany(c => c.Products)
               .Map(cs =>
               {
                   cs.MapLeftKey("ProductID");
                   cs.MapRightKey("CategoryID");
                   cs.ToTable("ProductCategory");
               });

            //modelBuilder.Entity<Product>()
            //  .HasMany<Size>(s => s.Sizes)
            //  .WithMany(c => c.Products)
            //  .Map(cs =>
            //  {
            //      cs.MapLeftKey("ProductID");
            //      cs.MapRightKey("SizesID");
            //      cs.ToTable("ProductSizes");
            //  });

            modelBuilder.Entity<Product>()
                .HasRequired<TypeProduct>(s => s.TypeProduct)
                .WithMany(g => g.Products)
                .HasForeignKey<int>(s => s.TypeProductID);

            modelBuilder.Entity<ProductShipping>()
                .HasRequired<Product>(s => s.Product)
                .WithMany(g => g.ProductShippings)
                .HasForeignKey<int>(s => s.ProductID);

            modelBuilder.Entity<ProductShipping>()
                .HasRequired<Product>(s => s.Product)
                 .WithMany(g => g.ProductShippings)
                 .HasForeignKey<int>(s => s.ProductID);

            modelBuilder.Entity<ProductShipping>()
                 .HasRequired<ShippingDetails>(s => s.ShippingDetails)
                 .WithMany(g => g.ProductShippings)
                 .HasForeignKey<int>(s => s.ShippingDetailsID);

            modelBuilder.Entity<ProductShipping>()
                .HasRequired<Size>(s => s.Size)
                .WithMany(g => g.ProductShippings)
                .HasForeignKey<int>(s => s.SizeID);

            modelBuilder.Entity<ProductShipping>()
               .HasRequired<Color>(s => s.Color)
               .WithMany(g => g.ProductShippings)
               .HasForeignKey<int>(s => s.ColorID);
        }
    }
}
