namespace ALB.Domain.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpirationInMinutes { get; set; }
    public required int RefreshExpirationInMinutes { get; set; }
    public required string Secret { get; set; }
}