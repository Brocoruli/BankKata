using System.Reflection;

namespace BankKata.Tests.Extensions;

public static class AssemblyExtensions
{
    public static string GetManifestResourceAsString(string resource) =>
        new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)).ReadToEnd();
}