namespace ALB.Api.Extensions;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int ExpirationInMinutes { get; init; }
    public required string Secret { get; init; }
}