using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.DTOs;

namespace TransactionMonitoring.API.Services;

public class AnalyticsService
{
    private readonly AppDbContext _context;

    public AnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    // 🔵 Risk Distribution
    public async Task<List<RiskDistributionDto>> GetRiskDistribution(string? accountId)
    {
        var query = _context.Alerts.AsQueryable();

        if (!string.IsNullOrEmpty(accountId))
        {
            query = query.Where(a => a.Transaction.AccountId == accountId);
        }

        return await query
            .GroupBy(a => a.RiskLevel)
            .Select(g => new RiskDistributionDto
            {
                RiskLevel = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
    }

    // 📈 Alerts per day
  public async Task<List<AlertsPerDayDto>> GetAlertsPerDay(string? accountId)
{
    var query = _context.Alerts
        .Include(a => a.Transaction)
        .AsQueryable();

    if (!string.IsNullOrEmpty(accountId))
    {
        query = query.Where(a => a.Transaction.AccountId == accountId);
    }

    // 🔥 Pull data into memory first
    var data = await query.ToListAsync();

    return data
        .GroupBy(a => a.CreatedAt.Date)
        .Select(g => new AlertsPerDayDto
        {
            Date = g.Key.ToString("yyyy-MM-dd"),
            Count = g.Count()
        })
        .OrderBy(x => x.Date)
        .ToList();
}
//Helper Method to get the risk level for a score
private string GetRiskLevel(int score)
{
    if (score >= 150) return "High";
    if (score >= 50) return "Medium";
    return "Low";
}

public async Task<List<HighRiskAccountDto>> GetHighRiskAccounts()
{
    var data = await _context.Alerts
        .Include(a => a.Transaction)
        .ToListAsync();

    var result = data
        .GroupBy(a => a.Transaction.AccountId)
        .Select(g =>
        {
            var totalScore = g.Sum(a => a.RiskScore);

            return new HighRiskAccountDto
            {
                AccountId = g.Key,
                TotalAlerts = g.Count(),
                TotalRiskScore = totalScore,
                RiskLevel = GetRiskLevel(totalScore)
            };
        })
        .OrderByDescending(x => x.TotalRiskScore)
        .ToList();

    return result;
}
}