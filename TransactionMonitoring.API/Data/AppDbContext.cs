using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Data;

//The bridge between your code and the database
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Alert> Alerts { get; set; }
}