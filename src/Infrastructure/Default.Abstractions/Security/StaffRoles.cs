namespace Default.Abstractions.Security;

public static class StaffRoles
{
    public const string AdminRu = "Администратор";
    public const string ModeratorRu = "Модератор";
    public const string EditorRu = "Редактор";

    public static bool IsAdminRole(string? roleName) =>
        !string.IsNullOrWhiteSpace(roleName) &&
        (roleName.Equals(AdminRu, StringComparison.OrdinalIgnoreCase) ||
         roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase));

    public static bool IsModeratorRole(string? roleName) =>
        !string.IsNullOrWhiteSpace(roleName) &&
        (roleName.Equals(ModeratorRu, StringComparison.OrdinalIgnoreCase) ||
         roleName.Equals("Moderator", StringComparison.OrdinalIgnoreCase));

    public static bool IsStaffRole(string? roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return false;

        return IsAdminRole(roleName) ||
               IsModeratorRole(roleName) ||
               roleName.Equals("Editor", StringComparison.OrdinalIgnoreCase) ||
               roleName.Equals(EditorRu, StringComparison.OrdinalIgnoreCase) ||
               roleName.Equals("ContentEditor", StringComparison.OrdinalIgnoreCase) ||
               roleName.Equals("Content_Editor", StringComparison.OrdinalIgnoreCase);
    }

    public static bool HasStaffRole(ISecurityContext securityContext) =>
        securityContext.IsAdmin || securityContext.Roles.Any(IsStaffRole);

    /// <summary>Admin or Moderator (not platform Editor).</summary>
    public static bool HasAdminOrModeratorRole(ISecurityContext securityContext) =>
        securityContext.IsAdmin ||
        securityContext.Roles.Any(r => IsAdminRole(r) || IsModeratorRole(r));
}
