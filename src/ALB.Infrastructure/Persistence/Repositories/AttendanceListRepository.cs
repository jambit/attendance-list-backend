using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories;

public class AttendanceListRepository(ApplicationDbContext dbContext) : IAttendanceListRepository
{
    public async Task<AttendanceList> CreateAsync(AttendanceList attendanceList, CancellationToken ct)
    {
        dbContext.AttendanceLists.Add(attendanceList);
        await dbContext.SaveChangesAsync(ct);
        return attendanceList;
    }

    public async Task<AttendanceList?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.AttendanceLists
            .Include(al => al.Cohort)
            .FirstOrDefaultAsync(al => al.Id == id, ct);
    }

    public async Task<IEnumerable<AttendanceList>> GetAllAsync(CancellationToken ct)
    {
        return await dbContext.AttendanceLists
            .Include(al => al.Cohort)
            .ToListAsync(ct);
    }

    public async Task UpdateAsync(AttendanceList attendanceList, CancellationToken ct)
    {
        dbContext.AttendanceLists.Update(attendanceList);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var attendanceList = await dbContext.AttendanceLists.FindAsync([id], ct);
        if (attendanceList is not null)
        {
            dbContext.AttendanceLists.Remove(attendanceList);
            await dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<IEnumerable<AttendanceListEntry>> GetAttendancesOfAttendanceListAsync(Guid attendanceListId,
        CancellationToken ct)
    {
        return await dbContext.AttendanceListEntries
            .Where(c => c.AttendanceListId == attendanceListId)
            .ToListAsync(ct);
    }
}