using ALB.Api.Models;
using ALB.Domain.Entities;

namespace ALB.Api.Extensions;

public static class AttendanceListExtension
{
    public static AttendanceListDto ToDto(this AttendanceList al)
    {
        return new AttendanceListDto(
            al.Id,
            al.CohortId,
            al.Open,
            al.ValidationPeriod.Start.ToDateTimeUnspecified(),
            al.ValidationPeriod.End.ToDateTimeUnspecified()
        );
    }
}