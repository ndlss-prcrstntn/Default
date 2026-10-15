namespace Default.Abstractions.Catalog;

/// <summary>Lookup of promo teams linked to an artist (implemented by Default.Promos).</summary>
public interface IArtistPromoTeamQuery
{
    Task<IReadOnlyList<ArtistPromoTeamRef>> GetPromoTeamsForArtistAsync(
        Guid artistId,
        CancellationToken cancellationToken = default);
}

public sealed record ArtistPromoTeamRef(Guid Id, string Name);
