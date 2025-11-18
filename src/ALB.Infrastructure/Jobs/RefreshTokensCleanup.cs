using ALB.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TickerQ.Utilities.Base;

namespace ALB.Infrastructure.Jobs;

/*
public class RefreshTokensCleanup(ILogger<RefreshTokensCleanup> logger, ApplicationDbContext dbContext)
{
    TickerFunction("CleanupExpiredRefreshTokens", "* 0/1 * * *")]
    public async ValueTask CleanupExpiredRefreshTokens(CancellationToken ct)
    {
        var deletedRows = await dbContext.RefreshTokens
            .Where(token => token.ExpiresOnUtc < DateTime.UtcNow.AddHours(1))
            .ExecuteDeleteAsync(ct);
        logger.LogTrace("{deletedRowCount} refresh tokens deleted.", deletedRows);
    }
}
*/