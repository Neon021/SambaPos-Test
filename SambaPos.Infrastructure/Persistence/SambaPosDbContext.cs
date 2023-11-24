using Microsoft.EntityFrameworkCore;
using SambaPos.Domain.Orders;

namespace SambaPos.Infrastructure.Persistence;
public sealed class SambaPosDbContext : DbContext
{
    public SambaPosDbContext(DbContextOptions<SambaPosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(SambaPosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
