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

    // Get all alerts
    [HttpGet]
    public async Task<IActionResult> GetAllAlerts()
    {
        var alerts = await _context.Alerts.ToListAsync();
        return Ok(alerts);
    }

    // Get alerts by account
    [HttpGet("by-account/{accountId}")]
    public async Task<IActionResult> GetAlertsByAccount(string accountId)
    {
        //Check if account exists
        var accountExists = await _context.Transactions
        .AnyAsync(t => t.AccountId ==accountId);

        if (!accountExists)
        {
            return NotFound(new {message = $"Account '{accountId}' not found"});
        }

        //Get alerts for that account
        var alerts = await _context.Alerts
            .Include(a => a.Transaction)
            .Where(a => a.Transaction.AccountId == accountId)
            .ToListAsync();

        return Ok(alerts);
    }

    // Get alerts by rule
    [HttpGet("by-rule/{ruleName}")]
    public async Task<IActionResult> GetAlertsByRule(string ruleName)
    {
        var alerts = await _context.Alerts
            .Where(a => a.RuleName == ruleName)
            .ToListAsync();

        if(!alerts.Any())
        {
            return NotFound(new {message = $"No alerts found for rule '{ruleName}'"});
        }

        return Ok(alerts);
    }
}