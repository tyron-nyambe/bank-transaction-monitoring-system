namespace TransactionMonitoring.API.DTOs;

public class HighRiskAccountDto
{
    public string AccountId { get; set; } = string.Empty;
    public int TotalAlerts { get; set; }
    public int TotalRiskScore { get; set; }
    public string RiskLevel { get; set; } = string.Empty;
}