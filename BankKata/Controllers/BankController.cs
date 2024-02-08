using BankKata.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankKata.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/")]
public class BankController : Controller
{
    private readonly AccountServices _accountServices;
    
    public BankController(AccountServices accountServices)
    {
        _accountServices = accountServices;
    }

    [HttpPost("Deposit")]
    public async Task<IActionResult> Deposit([FromBody] AccountRequest accountRequest)
    {
        _accountServices.Deposit(accountRequest);
        return Ok();
    }
    
    [HttpPost("Withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] AccountRequest accountRequest)
    {
        _accountServices.Withdraw(accountRequest);
        return Ok();
    }
    
    [HttpGet("GetAccount/{id}")]
    public async Task<IActionResult> GetAccount([FromRoute] Guid id, [FromServices] AccountRepository accountRepository)
    {
        var account = accountRepository.Find(id).Result;
        return Ok(account);
    }
}