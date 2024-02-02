using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Souq.com.Models;

namespace Souq.com.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<FAQ> faq { get; set; }
    }
}