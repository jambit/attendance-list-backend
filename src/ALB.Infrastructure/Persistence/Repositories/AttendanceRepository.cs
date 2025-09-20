using ALB.Domain.Entities;
using ALB.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using ALB.Domain.Repositories;
using NodaTime;

namespace ALB.Infrastructure.Persistence.Repositories;

public class AttendanceRepository(ApplicationDbContext dbContext) : IAttendanceRepository
{ 
    public async Task<AttendanceListEntry> CreateAsync(AttendanceListEntry entry, CancellationToken ct)
    {
        dbContext.AttendanceListEntries.Add(entry);
        await dbContext.SaveChangesAsync(ct);
        return entry;
    }
    
    public async Task<bool> ExistsAsync(Guid attendanceListId, Guid childId, LocalDate date, CancellationToken ct)
    {
        return await dbContext.AttendanceListEntries
            .AnyAsync(e => e.AttendanceListId == attendanceListId && 
                          e.ChildId == childId && 
                          e.Date == date, ct);
    }
    
    public async Task UpdateAsync(AttendanceListEntry attendanceListEntry, CancellationToken ct)
    {
        dbContext.AttendanceListEntries.Update(attendanceListEntry);
        await dbContext.SaveChangesAsync(ct);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entry = await dbContext.AttendanceListEntries.FindAsync([id], ct);

        if (entry is not null)
        {
            dbContext.AttendanceListEntries.Remove(entry);
            await dbContext.SaveChangesAsync(ct);
        }
    }
    
    public async Task<AttendanceListEntry?> GetByListChildAndDateAsync(Guid attendanceListId, Guid childId, LocalDate date, CancellationToken ct)
    {
        return await dbContext.AttendanceListEntries
            .Include(e => e.Child)
            .Include(e => e.AttendanceStatus)
            .Where(e => e.AttendanceListId == attendanceListId && e.Date == date)
            .FirstOrDefaultAsync(ct);
    }
}