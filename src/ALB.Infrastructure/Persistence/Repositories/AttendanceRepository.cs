using ALB.Domain.Entities;
using ALB.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using ALB.Domain.Repositories;

namespace ALB.Infrastructure.Persistence.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AttendanceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt,
        AttendanceStatus status, CancellationToken ct)
    {
        var attendance = await _dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date == date, ct);

        if (attendance is null)
        {
            attendance = new AttendanceListEntry
            {
                Id = Guid.NewGuid(),
                ChildId = childId,
                Date = date,
                ArrivalAt = arrivalAt,
                DepartureAt = departureAt,
                AttendanceStatus = status
            };

            _dbContext.Attendances.Add(attendance);
            
            await _dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt,
        AttendanceStatus status, CancellationToken ct)
    {
        var attendance = await _dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date == date, ct);

        if (attendance is not null)
        {
            attendance.ArrivalAt = arrivalAt;
            attendance.DepartureAt = departureAt;
            attendance.AttendanceStatus = status;
            _dbContext.Attendances.Update(attendance);
            
            await _dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task DeleteAsync(Guid childId, DateOnly date, CancellationToken ct)
    {
        var attendance = await _dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date == date, ct);

        if (attendance is null)
            throw new Exception("Attendance not found");

        _dbContext.Attendances.Remove(attendance);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task<AttendanceList> GetAttendanceListByIdAsync(Guid id)
    {
        return await _dbContext.AttendanceLists
            .Include(al => al.Cohort)
            .FirstOrDefaultAsync(al => al.Id == id);
    }


}
