using ALB.Api.Models;
using ALB.Domain.Entities;

namespace ALB.Api.Extensions;

public static class AttendanceListEntryExtension
{
    public static AttendanceListEntryDto ToDto(this AttendanceListEntry al)
    {
        return new AttendanceListEntryDto(
            al.Id,
            al.Date.ToDateOnly(),
            al.ArrivalAt.ToTimeOnly(),
            al.DepartureAt.ToTimeOnly(),
            al.AttendanceListId,
            al.AttendanceStatusId,
            al.ChildId
        );
    }
}