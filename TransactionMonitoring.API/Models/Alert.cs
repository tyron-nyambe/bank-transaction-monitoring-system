namespace TransactionMonitoring.API.Models;

public class Alert
{
    public int Id { get; set; } //Primary Key
    public int RiskScore { get; set; }
    public string RiskLevel { get; set; } = string.Empty;
    public Transaction Transaction { get; set; } = null!;
    public int TransactionId { get; set; } //Link to Transaction
    public string RuleName { get; set; } = string.Empty; //Which Rule triggered
    public DateTime CreatedAt { get; set; } //When was the alert created
}