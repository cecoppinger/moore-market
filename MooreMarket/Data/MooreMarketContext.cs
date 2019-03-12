using Microsoft.EntityFrameworkCore;
using MooreMarket.Models;

namespace MooreMarket.Data
{
  public class MooreMarketContext : DbContext
    {
        public MooreMarketContext(DbContextOptions<MooreMarketContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<Product> Products {get;set;}
        public DbSet<ProductCategory> Categories {get; set;}

        public DbSet<Message> Messages { get; set; }

        
    }
}
