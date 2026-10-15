namespace Default.Abstractions.Security;

/// <summary>
/// Shared role policy for catalog create / approve. Controllers and services use the same helpers.
/// </summary>
public static class CatalogAccessRoles
{
    /// <summary>Admin, Moderator, Editor — list staff queues and change PublicationStatus.</summary>
    public static bool CanModerateCatalog(ISecurityContext securityContext) =>
        StaffRoles.HasStaffRole(securityContext);

    /// <summary>
    /// Admin or Moderator only — create a new promo team (#227).
    /// Platform Editor / Verified / Partner cannot bootstrap promos.
    /// </summary>
    public static bool CanCreatePromoTeam(ISecurityContext securityContext) =>
        StaffRoles.HasAdminOrModeratorRole(securityContext);

    /// <summary>
    /// Staff or Verified — may create Pending Artist/Event/Venue without a managed promo.
    /// Does <strong>not</strong> include creating promo teams (see <see cref="CanCreatePromoTeam"/>).
    /// </summary>
    public static bool CanProposeCatalog(ISecurityContext securityContext) =>
        StaffRoles.HasStaffRole(securityContext) || VerifiedRoles.HasVerifiedRole(securityContext);

    /// <summary>Non-staff creator who should receive a management link after create.</summary>
    public static bool IsNonStaffCatalogCreator(ISecurityContext securityContext) =>
        !StaffRoles.HasStaffRole(securityContext);

    /// <summary>
    /// Non-staff, non-Verified — must create Artist/Event under a managed promo (Owner or Editor link).
    /// Platform Partner role is not required.
    /// </summary>
    public static bool IsPromoScopedCatalogCreator(ISecurityContext securityContext) =>
        !StaffRoles.HasStaffRole(securityContext) && !VerifiedRoles.HasVerifiedRole(securityContext);

    /// <summary>Obsolete alias — prefer <see cref="IsPromoScopedCatalogCreator"/>.</summary>
    public static bool IsPartnerOnly(ISecurityContext securityContext) =>
        IsPromoScopedCatalogCreator(securityContext);
}
