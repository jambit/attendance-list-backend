using ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

namespace ALB.Api.Endpoints.AttendanceList;

internal static class AttendanceListEndpointsGroup
{
    internal static void MapAttendanceListEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api/attendance-lists")
            .WithTags("Attendance Lists");

        group.AddAttendancePageEndpoint();
        group.AddCreateAttendanceListEntryEndpoint();
        //group.AddDeleteAttendanceListEntryEndpoint();
        group.AddUpdateAttendanceListEntryEndpoint();
    }
}