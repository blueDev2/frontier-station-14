using Content.Shared._NF.Marketplace.EntitySystems;

namespace Content.Shared._NF.Marketplace.Components;

[RegisterComponent]
public sealed partial class MarketplaceServerComponent : Component
{
    /// <summary>
    ///     All open Service Marketplace Entries by their id.
    /// </summary>
    [DataField("marketServiceEntries")]
    public Dictionary<int, MarketServiceEntry?> MarketServiceEntries = new();

    /// <summary>
    ///     All open Goods Marketplace Entries by their id.
    /// </summary>
    [DataField("marketGoodsEntries")]
    public Dictionary<int, MarketGoodEntry?> MarketGoodsEntries = new();
}
