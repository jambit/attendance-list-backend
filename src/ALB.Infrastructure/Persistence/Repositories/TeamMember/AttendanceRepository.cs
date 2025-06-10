using ALB.Domain.Entities;
using ALB.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories.TeamMember;

public class AttendanceRepository(ApplicationDbContext dbContext) : IAttendanceRepository
{
    public async Task CreateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt,
        AttendanceStatus status, CancellationToken ct)
    {
        var attendance = await dbContext.Attendances
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
                Status = status
            };

            dbContext.Attendances.Add(attendance);
            
            await dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateAsync(Guid childId, DateOnly date, TimeOnly? arrivalAt, TimeOnly? departureAt,
        AttendanceStatus status, CancellationToken ct)
    {
        var attendance = await dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date == date, ct);

        if (attendance is not null)
        {
            attendance.ArrivalAt = arrivalAt;
            attendance.DepartureAt = departureAt;
            attendance.Status = status;
            dbContext.Attendances.Update(attendance);
            
            await dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task DeleteAsync(Guid childId, DateOnly date, CancellationToken ct)
    {
        var attendance = await dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date == date, ct);

        if (attendance is null)
            throw new Exception("Attendance not found");

        dbContext.Attendances.Remove(attendance);
        await dbContext.SaveChangesAsync(ct);
    }
}