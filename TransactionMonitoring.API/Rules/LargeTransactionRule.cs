using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class LargeTransactionRule : IFraudRule
{
    public string RuleName => "Large Transaction";

    public bool IsMatch(Transaction transaction, List<Transaction> history)
    {
        return transaction.Amount > 50000;
    }
}