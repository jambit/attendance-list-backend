using ALB.Domain.Entities;
using NodaTime;

namespace ALB.Domain.Repositories;

public interface IAbsenceDayRepository
{
    Task AddAsync(AbsenceDay absenceDay);
    Task<bool> ExistsAsync(Guid childId, LocalDate date);
}