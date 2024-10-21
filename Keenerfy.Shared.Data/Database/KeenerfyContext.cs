using Microsoft.EntityFrameworkCore;
using Keenerfy.Models;
using Keenerfy.Shared.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Keenerfy.Database;
public class KeenerfyContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    //*local* public readonly string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=KeenerfyV2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    /* dev */ public readonly string connectionString = "Server=tcp:keenerfyserver.database.windows.net,1433;Initial Catalog=KeenerfyV2;Persist Security Info=False;User ID=guga;Password=MySqlDatabase123321@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }
}
