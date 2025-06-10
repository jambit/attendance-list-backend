using ALB.Api.Models;
using ALB.Domain.Identity;

namespace ALB.Api.Extensions;

public static class UserExtensions
{
    public static UserDto ToDto(this ApplicationUser user)
    {
        return new UserDto(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PhoneNumber
        );
    }
}