using ALB.Domain.Enum;
using NodaTime;
using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IAttendanceRepository
{
        Task<AttendanceListEntry> CreateAsync(AttendanceListEntry entry, CancellationToken ct);
        Task<bool> ExistsAsync(Guid attendanceListId, Guid childId, LocalDate date, CancellationToken ct);
        Task UpdateAsync(AttendanceListEntry attendanceListEntry, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
        Task<AttendanceListEntry?> GetByListChildAndDateAsync(Guid attendanceListId, Guid childId, LocalDate date,
                CancellationToken ct);
}