using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class RapidTransactionsRule : IFraudRule
{
    public string RuleName => "Rapid Transactions";

    public bool IsMatch(Transaction transaction, List<Transaction> history)
    {
        //Look for more than 3 transactions in the last 1 minute
        var recentTransactions = history
            .Where(t => (transaction.Timestamp - t.Timestamp).TotalMinutes <= 2)
            .ToList();

        return recentTransactions.Count >= 3;
    }
}