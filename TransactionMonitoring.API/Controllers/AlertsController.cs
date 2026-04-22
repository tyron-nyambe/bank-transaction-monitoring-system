using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.API.Data;

namespace TransactionMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlertsController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Get all alerts
    [HttpGet]
[HttpGet]
public async Task<IActionResult> GetAllAlerts()
{
    var alerts = await _context.Alerts
        .Include(a => a.Transaction)
        .Select(a => new
        {
            a.Id,
            a.RuleName,
            a.RiskScore,
            a.RiskLevel,
            a.CreatedAt,
            AccountId = a.Transaction.AccountId   // 🔥 KEY CHANGE
        })
        .ToListAsync();

    return Ok(alerts);
}

    // ✅ Get alerts by account
[HttpGet("by-account/{accountId}")]
public async Task<IActionResult> GetAlertsByAccount(string accountId)
{
    var alerts = await _context.Alerts
        .Include(a => a.Transaction)
        .Where(a => a.Transaction.AccountId == accountId)
        .Select(a => new
        {
            a.Id,
            a.RuleName,
            a.RiskScore,
            a.RiskLevel,
            a.CreatedAt,
            AccountId = a.Transaction.AccountId
        })
        .ToListAsync();

    if (!alerts.Any())
    {
        return NotFound(new { message = $"No alerts found for account '{accountId}'" });
    }

    return Ok(alerts);
}

    // ✅ Get alerts by rule
[HttpGet("by-rule/{ruleName}")]
public async Task<IActionResult> GetAlertsByRule(string ruleName)
{
    var alerts = await _context.Alerts
        .Include(a => a.Transaction)
        .Where(a => a.RuleName.Contains(ruleName))
        .Select(a => new
        {
            a.Id,
            a.RuleName,
            a.RiskScore,
            a.RiskLevel,
            a.CreatedAt,
            AccountId = a.Transaction.AccountId
        })
        .ToListAsync();

    if (!alerts.Any())
    {
        return NotFound(new { message = $"No alerts found for rule '{ruleName}'" });
    }

    return Ok(alerts);
}
}