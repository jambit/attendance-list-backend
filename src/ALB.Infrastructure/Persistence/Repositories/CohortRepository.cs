using ALB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ALB.Domain.Repositories;

namespace ALB.Infrastructure.Persistence.Repositories;

public class CohortRepository : ICohortRepository
{
    private readonly ApplicationDbContext _db;

    public CohortRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Cohort> CreateAsync(Cohort cohort)
    {
        _db.Cohorts.Add(cohort);
        await _db.SaveChangesAsync();
        return cohort;
    }

    public async Task<bool> ExistsAsync(int year, Guid groupId, Guid gradeId)
    {
        return await _db.Cohorts.AnyAsync(c =>
            c.CreationYear == year &&
            c.GroupId == groupId &&
            c.GradeId == gradeId
        );
    }
}