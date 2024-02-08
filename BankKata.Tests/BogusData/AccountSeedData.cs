using BankKata.Tests.Extensions;

namespace BankKata.Tests.BogusData;

public class AccountSeedData
{
    public static string AccountWithMovements() => AssemblyExtensions.GetManifestResourceAsString("BankKata.Tests.SeedData.AccountWithMovements.sql");
}