using BankKata;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var accountRepository = new AccountRepository();

builder.Services.AddSingleton(accountRepository);
builder.Services.AddSingleton(new AccountServices(accountRepository));

var app = builder.Build();

app.MapPost("Bank/Deposit/amount",(HttpContext context, AccountServices accountServices, AccountRequest accountRequest) =>
{
    accountServices.Deposit(accountRequest);
    context.Response.WriteAsync("Ok");
});

app.MapPost("Bank/Withdraw/amount",(HttpContext context, AccountRepository accountRepository, AccountRequest accountRequest) =>
{
    try
    {
        accountRepository.Withdraw(accountRequest);
    }
    catch (InvalidOperationException e)
    {
        return Results.BadRequest();
    }

    return Results.Ok();
});

app.MapGet("Bank/GetStatement/{id}", ([FromRoute] int id, HttpRequest request, AccountRepository accountRepository) =>
{
    return accountRepository.GetBalance(id);
});

app.Run();
public partial class Program { }