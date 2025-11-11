using ALB.Domain.Entities;
using ALB.Domain.Enum;

using NodaTime;

namespace ALB.Domain.Repositories;

public interface IAttendanceRepository
{
    Task CreateAsync(Guid childId, LocalDate date, LocalTime? arrivalAt, LocalTime? departureAt, AttendanceStatus status, CancellationToken ct);
    Task UpdateAsync(Guid childId, LocalDate date, LocalTime? arrivalAt, LocalTime? departureAt, AttendanceStatus status, CancellationToken ct);
    Task DeleteAsync(Guid childId, LocalDate date, CancellationToken ct);
    Task<AttendanceList?> GetAttendanceListByIdAsync(Guid id);
}