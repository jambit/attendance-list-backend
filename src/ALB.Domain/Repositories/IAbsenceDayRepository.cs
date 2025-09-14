using ALB.Domain.Entities;
using NodaTime;

namespace ALB.Domain.Repositories;

public interface IAbsenceDayRepository
{
    Task AddAsync(AbsenceDay absenceDay);
    Task<bool> ExistsAsync(Guid childId, LocalDate date);
    Task<IEnumerable<AbsenceDay>> GetByDateAsync(LocalDate date);
    Task AddRangeAsync(IEnumerable<AbsenceDay> absenceDays, CancellationToken cancellationToken);
    Task<bool> ExistsInRangeAsync(Guid childId, LocalDate startDate, LocalDate endDate, CancellationToken cancellationToken);
}