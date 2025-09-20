using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface ICohortRepository
{
    Task<Cohort> CreateAsync(Cohort cohort, CancellationToken ct);
    Task<bool> ExistsAsync(int year, Guid groupId, Guid gradeId, CancellationToken ct);
}

