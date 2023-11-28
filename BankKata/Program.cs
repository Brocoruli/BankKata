using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new AccountRepository());
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("Bank/Deposit/amount",(HttpContext context, AccountRepository accountRepository, int id, int amount) =>
{
    accountRepository.Deposit(id, amount);
    context.Response.WriteAsync("Ok");
});

app.MapGet("Bank/GetStatement", async context =>
{
    //var account = accountRepository.Get();
    //return account;
});

app.Run();
public partial class Program { }