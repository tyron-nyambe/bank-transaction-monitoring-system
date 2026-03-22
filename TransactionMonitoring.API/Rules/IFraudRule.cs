using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Rules;

public interface IFraudRule
{
    //bool IsMatch(Transaction transaction, List<Transaction> history);
    string RuleName { get; }

    int GetRiskScore(Transaction transaction, List<Transaction> history);

    
}