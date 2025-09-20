using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace ALB.Infrastructure.Persistence.Repositories;

public class AbsenceDayRepository(ApplicationDbContext dbContext) : IAbsenceDayRepository
{
    public async Task AddAsync(AbsenceDay absenceDay, CancellationToken ct)
    {
        dbContext.AbsenceDays.Add(absenceDay);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid childId, LocalDate date, CancellationToken ct)
    {
        return await dbContext.AbsenceDays
            .AnyAsync(a => a.ChildId == childId && a.Date == date, ct);
    }
    
    public async Task<IEnumerable<AbsenceDay>> GetByDateAsync(LocalDate date, CancellationToken ct)
    {
        return await dbContext.AbsenceDays
            .Where(a => a.Date == date)
            .ToListAsync(ct);
    }
    
    public async Task AddRangeAsync(IEnumerable<AbsenceDay> absenceDays, CancellationToken cancellationToken)
    {
        await dbContext.AbsenceDays.AddRangeAsync(absenceDays, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> ExistsInRangeAsync(Guid childId, LocalDate startDate, LocalDate endDate, CancellationToken cancellationToken)
    {
        return await dbContext.AbsenceDays
            .AnyAsync(ad => ad.ChildId == childId && ad.Date >= startDate && ad.Date <= endDate, cancellationToken);
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