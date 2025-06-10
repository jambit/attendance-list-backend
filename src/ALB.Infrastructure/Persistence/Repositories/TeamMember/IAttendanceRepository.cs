using ALB.Domain.Enum;

namespace ALB.Infrastructure.Persistence.Repositories.TeamMember;

public interface IAttendanceRepository
{
        Task CreateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt, AttendanceStatus status, CancellationToken ct);
        Task UpdateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt, AttendanceStatus status, CancellationToken ct);
        Task DeleteAsync(Guid childId, DateOnly date, CancellationToken ct);
}