using Microsoft.EntityFrameworkCore;
using SellingDreamsInfrastructure.Model;

namespace SellingDreamsInfrastructure;

public class InfrastructureDbContext : DbContext
{
  public DbSet<User> User { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Username=postgres;Password=password;Database=dreamdb");

  public override int SaveChanges()
  {
    var entities = ChangeTracker.Entries()
    .Where(entity => entity.Entity is IDbEntity &&
      (entity.State == EntityState.Added || entity.State == EntityState.Modified));
    foreach(var entity in entities)
    {
      if (entity.State == EntityState.Added)
        ((IDbEntity)entity.Entity).Created = DateTime.UtcNow;
      ((IDbEntity)entity.Entity).Updated = DateTime.UtcNow;
    }
    return base.SaveChanges();
  }
}
