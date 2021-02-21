using btg_pqr_back.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace btg_pqr_back.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PqrEntity> Pqr { get; set; }
        public DbSet<ClaimEntity> Claim { get; set; }
    }
}
