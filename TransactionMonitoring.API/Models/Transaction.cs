namespace TransactionMonitoring.API.Models;

public class Transaction
{

    public int Id { get; set; }              // Primary key (auto-generated)
    public string AccountId { get; set; } =string.Empty;   // Which account made the transaction
    public decimal Amount { get; set; }      // Money amount
    public string Currency { get; set; } = string.Empty;    // ZAR, USD, etc.
    public DateTime Timestamp { get; set; }  // When it happened
    public string Location { get; set; } =string.Empty;  // City or country
}