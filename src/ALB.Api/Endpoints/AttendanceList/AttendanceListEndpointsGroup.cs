using ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

namespace ALB.Api.Endpoints.AttendanceList;

internal static class AttendanceListEndpointsGroup
{
    internal static IEndpointRouteBuilder MapAttendanceListEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("/api/attendance-lists")
            .WithTags("Attendance Lists")
            .WithGroupName("AttendanceLists")
            .AddAttendancePageEndpoint()
            .AddCreateAttendanceListEntryEndpoint()
            .AddDeleteAttendanceListEntryEndpoint()
            .AddUpdateAttendanceListEntryEndpoint();
        
        return routeBuilder;
    }
}