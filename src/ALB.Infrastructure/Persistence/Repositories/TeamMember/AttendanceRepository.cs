using ALB.Domain.Entities;
using ALB.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories.TeamMember;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext dbContext;

    public AttendanceRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateOrUpdateAsync(Guid childId, DateTime date, DateTime? arrivalAt, DateTime? departureAt, ChildStatus status, CancellationToken ct)
    {
        var attendance = await dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date.Date == date.Date, ct);

        if (attendance is null)
        {
            attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                ChildId = childId,
                Date = date,
                ArrivalAt = arrivalAt,
                DepartureAt = departureAt,
                Status = status
            };

            dbContext.Attendances.Add(attendance);
        }
        else
        {
            attendance.ArrivalAt = arrivalAt;
            attendance.DepartureAt = departureAt;
            attendance.Status = status;
            dbContext.Attendances.Update(attendance);
        }

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid childId, DateTime date, CancellationToken ct)
    {
        var attendance = await dbContext.Attendances
            .FirstOrDefaultAsync(a => a.ChildId == childId && a.Date.Date == date.Date, ct);

        if (attendance is null)
            throw new Exception("Attendance not found");

        dbContext.Attendances.Remove(attendance);
        await dbContext.SaveChangesAsync(ct);
    }
}
