using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProductWebAPI.Models
{
    public class HealthCheckDB : DbContext
    {
        public HealthCheckDB(DbContextOptions<HealthCheckDB> options) : base(options)
        {

        }

        public DbSet<HealthCheck> HealthChecks { get; set; }
    }
}
