using Microsoft.EntityFrameworkCore;

namespace AndyCashflowAPI.Models
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions<LoanContext> options)
            : base(options)
        {
        }

        public DbSet<LoanItem> LoanItems { get; set; }
        public DbSet<PaymentPlanItem> PaymentPlanItems { get; set; }
    }
}