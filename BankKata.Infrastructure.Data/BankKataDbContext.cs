using BankKata.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankKata.Infrastructure.Data;

public class BankKataDbContext : DbContext
{
    public BankKataDbContext(DbContextOptions<BankKataDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Movement> Movements { get; set; }

}