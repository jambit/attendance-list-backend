using ALB.Domain.Enum;

namespace ALB.Infrastructure.Persistence.Adapters.TeamMember;

public interface IAttendanceAdapter
{
    
        Task CreateOrUpdateAsync(Guid childId, DateTime date, DateTime? arrivalAt, DateTime? departureAt, ChildStatus status, CancellationToken ct);
        Task DeleteAsync(Guid childId, DateTime date, CancellationToken ct);
    

}