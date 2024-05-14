using AuctionService.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Reflection;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext, IUnitOfWork
{
    public AuctionDbContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<Auction> Auction => Set<Auction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties()
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
        {
            property.SetColumnType("decimal(18,2)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> CompleteAsync() =>
       await base.SaveChangesAsync();
}
