using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Models;
using TransactionMonitoring.API.Rules;

namespace TransactionMonitoring.API.Services;

public class TransactionService
{
    private readonly AppDbContext _context;
    private readonly List<IFraudRule> _rules;

    public TransactionService(AppDbContext context)
    {
        _context = context;

        //Registering rules
        _rules = new List<IFraudRule>
        {
            new LargeTransactionRule(),
            new RapidTransactionsRule(),
            new LocationMismatchRule()
        };
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        //Save transaction first
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        //Get transaction history for this account
        var history = _context.Transactions
        .Where(t=> t.AccountId == transaction.AccountId )
        .ToList();

        //Apply rules
        foreach (var rule in _rules)
        {
            if (rule.IsMatch(transaction, history))
            {
                var alert = new Alert
                {
                    TransactionId = transaction.Id,
                    RuleName = rule.RuleName,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Alerts.Add(alert);
            }
        }

        await _context.SaveChangesAsync();

        return transaction;
    }
}