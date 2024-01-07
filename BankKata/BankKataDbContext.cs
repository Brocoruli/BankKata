using Microsoft.EntityFrameworkCore;

namespace BankKata;

public class BankKataDbContext : DbContext
{
    public BankKataDbContext(DbContextOptions<BankKataDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Movement> Movements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasData(
            new Account(1, 0),
            new Account(2, 500)
            );
        modelBuilder.Entity<Movement>().HasData(
            new Movement(1,1,0,0),
            new Movement(2,2,500,500)
        );
    }
}