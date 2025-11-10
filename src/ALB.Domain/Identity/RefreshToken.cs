namespace ALB.Domain.Identity;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Value { get; set; }
    public required DateTime ExpiresOnUtc { get; set; }
    
    public ApplicationUser User { get; set; } = null!;
}