using Microsoft.AspNetCore.Mvc;
using TransactionMonitoring.API.Services;

namespace TransactionMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _service;

    public AnalyticsController(AnalyticsService service)
    {
        _service = service;
    }

    // 🔵 Risk Distribution
    [HttpGet("risk-distribution")]
    public async Task<IActionResult> GetRiskDistribution([FromQuery] string? accountId)
    {
        var result = await _service.GetRiskDistribution(accountId);
        return Ok(result);
    }

    // 📈 Alerts per day
    [HttpGet("alerts-per-day")]
    public async Task<IActionResult> GetAlertsPerDay([FromQuery] string? accountId)
    {
        var result = await _service.GetAlertsPerDay(accountId);
        return Ok(result);
    }

    [HttpGet("high-risk-accounts")]
    public async Task<IActionResult> GetHighRiskAccounts()
    {
        var result = await _service.GetHighRiskAccounts();
        return Ok(result);
    }

}