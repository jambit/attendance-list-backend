using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IAttendanceListRepository
{
    Task<AttendanceList> CreateAsync(AttendanceList attendanceList, CancellationToken ct = default);
    Task<AttendanceList?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<AttendanceList>> GetAllAsync(CancellationToken ct = default);
    Task UpdateAsync(AttendanceList attendanceList, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}