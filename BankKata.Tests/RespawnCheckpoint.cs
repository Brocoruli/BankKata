using Respawn;

namespace BankKata.Tests;

public static class RespawnCheckpoint
{
    public static Checkpoint Checkpoint()
    {
        return new Checkpoint
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
            TablesToIgnore = new[] { "__EFMigrationsHistory" }
        };
    }
}