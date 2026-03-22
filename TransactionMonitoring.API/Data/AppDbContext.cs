using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Data;

//The bridge between code and the database
public class AppDbContext : DbContext
{
    //Contructor used by Dependency Injection
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    //These represent tables in the database.
    public DbSet<Transaction> Transactions { get; set; } //Create table Transactions based on the Transactions model.
    public DbSet<Alert> Alerts { get; set; } //Create table Alerts based on the Alerts model
}