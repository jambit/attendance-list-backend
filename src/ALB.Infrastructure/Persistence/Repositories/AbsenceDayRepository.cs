using ALB.Domain.Entities;
using ALB.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

using NodaTime;

namespace ALB.Infrastructure.Persistence.Repositories;

public class AbsenceDayRepository : IAbsenceDayRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AbsenceDayRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(AbsenceDay absenceDay)
    {
        _dbContext.AbsenceDays.Add(absenceDay);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid childId, LocalDate date)
    {
        return await _dbContext.AbsenceDays
            .AnyAsync(a => a.ChildId == childId && a.Date == date);
    }

    public async Task<IEnumerable<AbsenceDay>> GetByDateAsync(LocalDate date)
    {
        return await _dbContext.AbsenceDays
            .Where(a => a.Date == date)
            .ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<AbsenceDay> absenceDays, CancellationToken cancellationToken)
    {
        await _dbContext.AbsenceDays.AddRangeAsync(absenceDays, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsInRangeAsync(Guid childId, LocalDate startDate, LocalDate endDate, CancellationToken cancellationToken)
    {
        return await _dbContext.AbsenceDays
            .AnyAsync(ad => ad.ChildId == childId && ad.Date >= startDate && ad.Date <= endDate, cancellationToken);
    }
}