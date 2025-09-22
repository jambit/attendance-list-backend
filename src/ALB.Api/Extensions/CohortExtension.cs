using ALB.Api.Models;
using ALB.Domain.Entities;

namespace ALB.Api.Extensions;

public static class CohortExtension
{
    public static CohortDto ToDto(this Cohort cohort)
    {
        return new CohortDto(
            cohort.Id,
            cohort.CreationYear,
            cohort.GroupId,
            cohort.GradeId
        );
    }
}