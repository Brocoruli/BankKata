using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new AccountRepository());
var app = builder.Build();

app.MapPost("Bank/Deposit/amount",(HttpContext context, AccountRepository accountRepository, AccountRequest accountRequest) =>
{
    accountRepository.Deposit(accountRequest.id, accountRequest.amount);
    context.Response.WriteAsync("Ok");
});

app.MapGet("Bank/GetStatement/{id}", ([FromRoute] int id, HttpRequest request, AccountRepository accountRepository) =>
{
    return accountRepository.GetBalance(id);
});

app.Run();
public partial class Program { }