namespace Default.Abstractions.Security;

public static class PartnerRoles
{
    public const string PartnerRu = "Партнёр";

    public static bool IsPartnerRole(string? roleName) =>
        !string.IsNullOrWhiteSpace(roleName) &&
        (roleName.Equals(PartnerRu, StringComparison.OrdinalIgnoreCase) ||
         roleName.Equals("Partner", StringComparison.OrdinalIgnoreCase));

    public static bool HasPartnerRole(ISecurityContext securityContext) =>
        securityContext.Roles.Any(IsPartnerRole);
}
