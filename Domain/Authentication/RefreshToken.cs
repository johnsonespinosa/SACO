namespace Domain.Authentication;

public sealed class RefreshToken
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string UserId { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public DateTimeOffset Expire { get; init; } = DateTimeOffset.UtcNow.AddDays(7);
    public bool IsExpired => DateTimeOffset.UtcNow >= Expire;
    public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
    public string CreatedByIp { get; init; } = string.Empty;
    public DateTimeOffset? Revoked { get; set; }  // Nullable
    public string? RevokedByIp { get; set; }  // Nullable
    public string? ReplacedByToken { get; set; }  // Nullable
    public bool IsActive => !IsExpired && Revoked == null;
    public bool IsRevoked => Revoked != null;

    public void Revoke(string ipAddress, string replacedByToken)
    {
        Revoked = DateTimeOffset.UtcNow;
        RevokedByIp = ipAddress;
        ReplacedByToken = replacedByToken;
    }
}