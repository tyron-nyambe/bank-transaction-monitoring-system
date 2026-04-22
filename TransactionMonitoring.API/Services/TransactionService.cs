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

    private string GetRiskLevel(int score)
    {
        if (score >= 70) return "High";
        if (score >= 30) return "Medium";
        return "Low";
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        //Save transaction first
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        int totalRiskScore = 0;
        var triggeredRules = new List<string>();

        //Get transaction history for this account
        var history = _context.Transactions
        .Where(t=> t.AccountId == transaction.AccountId )
        .ToList();

        foreach (var rule in _rules)
        {
            var score = rule.GetRiskScore(transaction, history);

            if (score > 0)
            {
                totalRiskScore += score;
                triggeredRules.Add(rule.RuleName);
            }
        }

        //Apply rules
       if (totalRiskScore > 0)
        {
        var alert = new Alert
        {
            TransactionId = transaction.Id, 
            RuleName = string.Join(", ", triggeredRules),
            CreatedAt = DateTime.UtcNow,
            RiskScore = totalRiskScore,
            RiskLevel = GetRiskLevel(totalRiskScore)
        };

        _context.Alerts.Add(alert);

        await _context.SaveChangesAsync();
    }

        return transaction;
    }
}