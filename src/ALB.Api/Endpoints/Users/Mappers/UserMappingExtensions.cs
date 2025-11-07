using ALB.Api.Models;

namespace ALB.Api.Endpoints.Users.Mappers;

internal static class UserMappingExtensions
{
    internal static GetUsersResponse ToResponse(this List<UserDto> users)
    {
        return new GetUsersResponse(users);
    }
}