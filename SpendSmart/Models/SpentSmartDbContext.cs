using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpentSmartDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public SpentSmartDbContext(DbContextOptions<SpentSmartDbContext> options)
            : base(options)
        {

        }
    }
}
