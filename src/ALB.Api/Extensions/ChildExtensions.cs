using ALB.Api.Models;
using ALB.Domain.Entities;

namespace ALB.Api.Extensions;

public static class ChildExtensions
{
    public static ChildDto ToDto(this Child child)
    {
        return new ChildDto(
            child.Id, 
            child.FirstName, 
            child.LastName, 
            child.DateOfBirth.ToDateOnly(), 
            child.GroupId);
    }
}