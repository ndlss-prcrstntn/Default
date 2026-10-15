using Default.DataAccess.Config;
using Default.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Default.DataAccess;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IOptions<DataAccessConfigSection> config)
    : ServiceDbContext(options, config)
{
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        NormalizeCatalogSearchNames();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        NormalizeCatalogSearchNames();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Keeps NameNormalized / NicknameNormalized aligned with the display name
    /// for entities that define those columns.
    /// </summary>
    private void NormalizeCatalogSearchNames()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified))
                continue;

            CopyNormalized(entry, "Name", "NameNormalized");
            CopyNormalized(entry, "Nickname", "NicknameNormalized");
        }
    }

    private static void CopyNormalized(EntityEntry entry, string source, string target)
    {
        if (entry.Metadata.FindProperty(source) is null || entry.Metadata.FindProperty(target) is null)
            return;

        var raw = entry.Property(source).CurrentValue as string;
        entry.Property(target).CurrentValue = SearchText.Normalize(raw);
    }
}
