using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class RapidTransactionsRule : IFraudRule
{
    public string RuleName => "Rapid Transactions";

    public int GetRiskScore(Transaction transaction, List<Transaction> history)
{
    var recentTransactions = history
        .Where(t => (transaction.Timestamp - t.Timestamp).TotalMinutes <= 1)
        .ToList();

    if (recentTransactions.Count >= 3)
        return 60;

    return 0;
}
}