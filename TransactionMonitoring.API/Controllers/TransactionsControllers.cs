using Microsoft.AspNetCore.Mvc;
using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Models;

namespace TransactionMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context) //Dependency injection here giving us a working database connection
    {
        _context = context;
    }

    [HttpPost]
   [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
    {
        _context.Transactions.Add(transaction); //adds transaction to EF
        await _context.SaveChangesAsync(); //Saves it into databse

        return Ok(transaction);
    }
}