using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Infrastructure.Persistence;
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
}