using ALB.Domain.Enum;

namespace ALB.Infrastructure.Persistence.Repositories.TeamMember;

public interface IAttendanceRepository
{
    
        Task CreateOrUpdateAsync(Guid childId, DateTime date, DateTime? arrivalAt, DateTime? departureAt, ChildStatus status, CancellationToken ct);
        Task DeleteAsync(Guid childId, DateTime date, CancellationToken ct);
    

}