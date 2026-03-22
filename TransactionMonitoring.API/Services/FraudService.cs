using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Models;
using TransactionMonitoring.API.Rules;


namespace TransactionMonitoring.API.Services;

public class FraudService
{
    private readonly AppDbContext _context;
    private readonly List<IFraudRule> _rules;

    public FraudService(AppDbContext context)
    {
        _context = context;
        // _rules = new List<IFraudRule>
        // {
        //     new LargeTransactionRule()
        // };
        _rules = new List<IFraudRule>();
    }

    public async Task EvaluateTransaction(Transaction tx)
    {
        var history = _context.Transactions
            .Where(t => t.AccountId == tx.AccountId)
            .ToList();

        foreach (var rule in _rules)
        {
            // if (rule.IsMatch(tx, history))
            // {
            //     var alert = new Alert
            //     {
            //         TransactionId = tx.Id,
            //         RuleName = rule.RuleName,
            //         CreatedAt = DateTime.UtcNow
            //     };

            //     _context.Alerts.Add(alert);
            // }
        }

        await _context.SaveChangesAsync();
    }
}   