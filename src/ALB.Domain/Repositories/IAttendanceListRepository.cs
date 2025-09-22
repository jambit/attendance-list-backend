using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IAttendanceListRepository
{
    Task<AttendanceList> CreateAsync(AttendanceList attendanceList, CancellationToken ct);
    Task<AttendanceList?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<AttendanceList>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(AttendanceList attendanceList, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<AttendanceListEntry>> GetAttendancesOfAttendanceListAsync(Guid attendanceListId, CancellationToken ct);
}