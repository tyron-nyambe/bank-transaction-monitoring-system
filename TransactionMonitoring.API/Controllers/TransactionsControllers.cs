using Microsoft.AspNetCore.Mvc;
using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Models;
using TransactionMonitoring.API.Services;

namespace TransactionMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionsController(TransactionService transactionService) //Dependency injection here giving us a working database connection
    {
        _transactionService = transactionService;
    }

    [HttpPost]
   [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
    {
        var result = await _transactionService.CreateTransactionAsync(transaction);

        return Ok(result);
    }
}