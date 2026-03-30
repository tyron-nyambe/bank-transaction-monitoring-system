namespace TransactionMonitoring.API.DTOs;

public class AlertsPerDayDto
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
}