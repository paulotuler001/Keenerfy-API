using Microsoft.EntityFrameworkCore;
using Keenerfy.Models;
using Keenerfy.Shared.Models.Models;

namespace Keenerfy.Database;
public class KeenerfyContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public readonly string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=KeenerfyV2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }
}
