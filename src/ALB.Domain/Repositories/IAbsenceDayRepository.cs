using ALB.Domain.Entities;
using NodaTime;

namespace ALB.Domain.Repositories;

public interface IAbsenceDayRepository
{
    Task AddAsync(AbsenceDay absenceDay, CancellationToken ct);
    Task<bool> ExistsAsync(Guid childId, LocalDate date, CancellationToken ct);
    Task<IEnumerable<AbsenceDay>> GetByDateAsync(LocalDate date, CancellationToken ct);
}