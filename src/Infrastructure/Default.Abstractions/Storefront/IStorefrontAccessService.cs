namespace Default.Abstractions.Storefront;

/// <summary>
/// Enforces promo-scoped catalog visibility. When <see cref="IStorefrontContext.IsScoped"/> is false,
/// all checks are no-ops / unrestricted.
/// </summary>
public interface IStorefrontAccessService
{
    Task ApplyEventFilterAsync(IStorefrontEventFilterTarget filter, CancellationToken cancellationToken = default);

    Task ApplyArtistFilterAsync(IStorefrontArtistFilterTarget filter, CancellationToken cancellationToken = default);

    Task ApplyPromoFilterAsync(IStorefrontPromoFilterTarget filter, CancellationToken cancellationToken = default);

    Task ApplyVenueFilterAsync(IStorefrontVenueFilterTarget filter, CancellationToken cancellationToken = default);

    Task<bool> IsEventVisibleAsync(Guid eventId, CancellationToken cancellationToken = default);

    Task<bool> IsArtistVisibleAsync(Guid artistId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Other promo teams remain viewable (collaborations). Always true when unscoped;
    /// when scoped, any existing promo id is allowed (list is narrowed separately).
    /// </summary>
    Task<bool> IsPromoVisibleAsync(Guid promoTeamId, CancellationToken cancellationToken = default);

    Task<bool> IsVenueVisibleAsync(Guid venueId, CancellationToken cancellationToken = default);

    /// <summary>
    /// When scoped, artist UIDs on the storefront promo roster; otherwise empty.
    /// Used to pin roster artists at the top of an event lineup.
    /// </summary>
    Task<IReadOnlySet<Guid>> GetScopedRosterArtistIdsAsync(CancellationToken cancellationToken = default);
}

/// <summary>Narrow surface so catalog filters can be applied without module coupling.</summary>
public interface IStorefrontEventFilterTarget
{
    Guid? PromoTeamId { get; set; }

    /// <summary>
    /// When set (artist card / tour), storefront event scope is not applied —
    /// the artist’s full public schedule is shown even on a promo storefront.
    /// </summary>
    Guid? ArtistId { get; }

    /// <summary>When set, restrict to these event UIDs (e.g. storefront scope or liked).</summary>
    IReadOnlyCollection<Guid>? TargetUids { get; set; }
}

public interface IStorefrontArtistFilterTarget
{
    IReadOnlyCollection<Guid>? TargetUids { get; set; }
}

public interface IStorefrontPromoFilterTarget
{
    IReadOnlyCollection<Guid>? TargetUids { get; set; }
}

public interface IStorefrontVenueFilterTarget
{
    IReadOnlyCollection<Guid>? TargetUids { get; set; }
}
