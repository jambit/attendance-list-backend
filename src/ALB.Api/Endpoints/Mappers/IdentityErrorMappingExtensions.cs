using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Mappers;

internal static class IdentityErrorMappingExtensions
{
    internal static String AsErrorString(this IEnumerable<IdentityError> errors)
    {
        List<IdentityError> errorList = errors.ToList();
        return string.Join(", ", errorList.Select(e => e.Description));
    }
}