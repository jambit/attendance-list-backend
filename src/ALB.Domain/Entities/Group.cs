using System.Text.Json.Serialization;
using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class Group
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid ResponsibleUserId { get; set; }
    
    public ApplicationUser ResponsibleUser { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Child> Children { get; set; } = new List<Child>();
    [JsonIgnore]
    public ICollection<Cohort> Cohorts { get; set; } = new List<Cohort>();
    [JsonIgnore]
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}