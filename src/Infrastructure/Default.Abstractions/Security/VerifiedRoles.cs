namespace Default.Abstractions.Security;

/// <summary>
/// «Проверенный» — can propose catalog cards (Pending). Not staff; cannot approve.
/// </summary>
public static class VerifiedRoles
{
    public const string VerifiedRu = "Проверенный";

    public static bool IsVerifiedRole(string? roleName) =>
        !string.IsNullOrWhiteSpace(roleName) &&
        (roleName.Equals(VerifiedRu, StringComparison.OrdinalIgnoreCase) ||
         roleName.Equals("Verified", StringComparison.OrdinalIgnoreCase));

    public static bool HasVerifiedRole(ISecurityContext securityContext) =>
        securityContext.Roles.Any(IsVerifiedRole);
}
