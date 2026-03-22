using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Services;

public class TransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}