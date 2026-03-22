using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class LocationMismatchRule : IFraudRule
{
    public string RuleName => "Location Mismatch";

    public bool IsMatch(Transaction transaction, List<Transaction> history)
    {
        //If the account has past transactions, check if current location is different from majority
        if (!history.Any()) return false;

        var mostCommonLocation = history
            .GroupBy(t => t.Location)
            .OrderByDescending(g => g.Count())
            .First().Key;

        return transaction.Location != mostCommonLocation;
    }
}