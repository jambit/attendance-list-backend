using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ExistsAsync(Guid childId, DateOnly date)
    {
        return await _dbContext.AbsenceDays
            .AnyAsync(a => a.ChildId == childId && a.Date == date);
    }
}