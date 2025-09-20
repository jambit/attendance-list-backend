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
}