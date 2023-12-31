using BankKata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddDbContext<BankKataDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

//var accountRepository = new AccountRepository(new BankKataDbContext());
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddSingleton(accountRepository);
builder.Services.AddScoped<AccountServices>();
//builder.Services.AddSingleton(new AccountServices(accountRepository));



var app = builder.Build();

app.MapPost("Bank/Deposit/amount",(HttpContext context, [FromBody]  AccountRequest accountRequest, AccountServices accountServices) =>
{
    accountServices.Deposit(accountRequest);
    context.Response.WriteAsync("Ok");
});

app.MapPost("Bank/Withdraw/amount",(HttpContext context, [FromBody] AccountRequest accountRequest, AccountServices accountServices) =>
{
    try
    {
        accountServices.Withdraw(accountRequest);
    }
    catch (InvalidOperationException e)
    {
        return Results.BadRequest();
    }

    return Results.Ok();
});

app.MapGet("Bank/GetStatement/{id}", ([FromServices] AccountRepository accountRepository, [FromRoute] int id, HttpRequest request) =>
{
    return accountRepository.GetBalance(id);
});

app.Run();
public partial class Program { }