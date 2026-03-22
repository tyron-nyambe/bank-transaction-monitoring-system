using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class LocationMismatchRule : IFraudRule
{
    public string RuleName => "Location Mismatch";

    public int GetRiskScore(Transaction transaction, List<Transaction> history)
{
    if (!history.Any()) return 0;

    var mostCommonLocation = history
        .GroupBy(t => t.Location)
        .OrderByDescending(g => g.Count())
        .First().Key;

    if (transaction.Location != mostCommonLocation)
        return 50;

    return 0;
}
}