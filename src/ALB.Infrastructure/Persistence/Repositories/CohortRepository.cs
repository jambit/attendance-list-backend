using ALB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ALB.Domain.Repositories;

namespace ALB.Infrastructure.Persistence.Repositories;

public class CohortRepository(ApplicationDbContext db) : ICohortRepository
{
    public async Task<Cohort> CreateAsync(Cohort cohort, CancellationToken ct)
    {
        db.Cohorts.Add(cohort);
        await db.SaveChangesAsync(ct);
        return cohort;
    }

    public async Task<bool> ExistsAsync(int year, Guid groupId, Guid gradeId, CancellationToken ct)
    {
        return await db.Cohorts.AnyAsync(c =>
            c.CreationYear == year &&
            c.GroupId == groupId &&
            c.GradeId == gradeId, ct
        );
    }
}