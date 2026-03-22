using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public class LargeTransactionRule : IFraudRule
{
    public string RuleName => "Large Transaction";

    public int GetRiskScore(Transaction transaction, List<Transaction> history)
{
    if (transaction.Amount > 50000)
        return 80;

    return 0;
}
}