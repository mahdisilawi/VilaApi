using Microsoft.EntityFrameworkCore;

namespace Vila.WebApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Models.Vila> Vilas { get; set; }
        public DbSet<Models.Detail> Details { get; set; }
    }
    
}
