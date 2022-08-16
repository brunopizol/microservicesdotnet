using Microsoft.EntityFrameworkCore;

namespace VShop.ProductApi.Context
{
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
