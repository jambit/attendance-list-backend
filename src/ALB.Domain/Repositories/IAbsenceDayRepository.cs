using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IAbsenceDayRepository
{
    Task AddAsync(AbsenceDay absenceDay);
    Task<bool> ExistsAsync(Guid childId, DateOnly date);
}