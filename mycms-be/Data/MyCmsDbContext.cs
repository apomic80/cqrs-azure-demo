using Microsoft.EntityFrameworkCore;
using mycms.Data.Entities;

namespace mycms.Data
{
    public class MyCmsDbContext : DbContext
    {
        public MyCmsDbContext(DbContextOptions options)
            : base(options) { }

        public virtual DbSet<Article> Articles { get; set; }
        
    }
}