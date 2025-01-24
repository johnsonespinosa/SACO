namespace Domain.Entities;

public sealed class RefreshSecurityToken
{
    public Guid Id { get; init; }
    public string? JwtSecurityToken { get; init; }
    public DateTime Expire { get; init; }
    public bool IsExpired => DateTime.Now >= Expire;
    public DateTime Created { get; init; }
    public string? CreatedByIp { get; init; }
    public DateTime? Revoked { get; init; }
    public string? RevokedByIp { get; init; }
    public string? ReplacedBySecurityToken { get; init; }
    public bool IsActive => Revoked == null && !IsExpired;
}