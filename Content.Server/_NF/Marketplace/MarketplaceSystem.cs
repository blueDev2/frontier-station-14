
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Content.Server.GameTicking.Events;
using Content.Shared._NF.Marketplace.Components;
using Content.Shared._NF.Marketplace.EntitySystems;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Utility;

namespace Content.Server._NF.Marketplace;

public sealed partial class MarketplaceSystem : SharedMarketplaceSystem
{
    private const int MaxAttempts = 10;
    [Dependency] private readonly EntityManager _entityManager = default!;
    [Dependency] private readonly IRobustRandom _robustRandom = default!;
    public override void Initialize()
    {
        base.Initialize();
        //_sawmill = Logger.GetSawmill("marketplace.entries");

        SubscribeLocalEvent<RoundStartingEvent>(OnRoundStarting);
        //InitializeUi();
    }

    private void OnRoundStarting(RoundStartingEvent ev)
    {
        var query = EntityQuery<MarketplaceServerComponent>();
        foreach (var server in query)
        {
            RemCompDeferred(server.Owner, server);
        }

        // use nullspace entity to store all information about contracts
        var uid = Spawn(null, MapCoordinates.Nullspace);
        EnsureComp<MarketplaceServerComponent>(uid);
    }


    /// <summary>
    /// Gets the MarketServer if one exists.
    /// </summary>
    private bool TryGetMarketServer([NotNullWhen(true)] out MarketplaceServerComponent? marketServer)
    {
        marketServer = null;
        var serverList = EntityQuery<MarketplaceServerComponent>();
        if (!serverList.Any())
        {
            return false;
        }
        marketServer = serverList.First();
        return true;
    }

    /// <summary>
    /// Gets the Market Good Entry with the id entryId if it exists.
    /// </summary>
    public bool TryGetMarketGoodEntry(int entryId, [NotNullWhen(true)] out MarketGoodEntry? marketGoodEntry)
    {
        marketGoodEntry = null;
        if (TryGetMarketServer(out var marketServer))
        {
            if (marketServer.MarketGoodsEntries.TryGetValue(entryId, out var possibleMarketGoodEntry))
            {
                marketGoodEntry = possibleMarketGoodEntry;
                return true;
            }
            marketGoodEntry = null;
            return false;
        }
        return false;
    }

    /// <summary>
    /// Gets the Market Service Entry with the id entryId if it exists.
    /// </summary>
    public bool TryGetMarketServiceEntry(int entryId, [NotNullWhen(true)] out MarketServiceEntry? marketServiceEntry)
    {
        marketServiceEntry = null;
        if (TryGetMarketServer(out var marketServer))
        {
            if (marketServer.MarketServiceEntries.TryGetValue(entryId, out var possibleMarketServiceEntry))
            {
                marketServiceEntry = possibleMarketServiceEntry;
                return true;
            }
            marketServiceEntry = null;
            return false;
        }
        return false;
    }

    public bool TryCreateMarketGoodEntry(
    Agent agent,
    [NotNullWhen(true)] out int? newId,
    List<EntityPrototype> goods,
    string associatedVessel = "",
    int price = 0,
    string description = "",
    int quantity = 1,
    List<MarketGoodEntryCategory>? goodsCategories = null)
    {
        goodsCategories ??= new();

        newId = null;
        if (TryGetMarketServer(out var marketServer))
        {
            var randomId = _robustRandom.Next(0, int.MaxValue);
            for (var i = 0; i < MaxAttempts + 1; ++i)
            {
                if (!TryGetMarketGoodEntry(randomId, out _))
                {
                    break;
                }
                if (i == MaxAttempts)
                {
                    newId = null;
                    return false;
                }
                randomId = _robustRandom.Next(0, int.MaxValue);
            }
            var newGoodEntry = new MarketGoodEntry(agent, associatedVessel, price, description, quantity, goodsCategories, goods);
            marketServer.MarketGoodsEntries.Add(randomId, newGoodEntry);
            newId = randomId;
            return true;
        }
        return false;
    }

    public bool TryCreateMarketServiceEntry(
    Agent agent,
    [NotNullWhen(true)] out int? newId,
    string associatedVessel = "",
    int price = 0,
    string description = "",
    List<MarketServiceEntryCategory>? serviceCategories = null)
    {
        serviceCategories ??= new();

        newId = null;
        if (TryGetMarketServer(out var marketServer))
        {
            int randomId = _robustRandom.Next(0, int.MaxValue);
            for (var i = 0; i < MaxAttempts + 1; ++i)
            {
                if (!TryGetMarketGoodEntry(randomId, out _))
                {
                    break;
                }
                if (i == MaxAttempts)
                {
                    newId = null;
                    return false;
                }
                randomId = _robustRandom.Next(0, int.MaxValue);
            }
            var newServiceEntry = new MarketServiceEntry(agent, associatedVessel, price, description, serviceCategories);
            marketServer.MarketServiceEntries.Add(randomId, newServiceEntry);
            newId = randomId;
            return true;
        }
        return false;
    }

    public bool TryRemoveMarketGoodEntry(int entryId)
    {
        if (TryGetMarketServer(out var marketServer))
        {
            return marketServer.MarketGoodsEntries.Remove(entryId);
        }
        return false;
    }

    public bool TryRemoveMarketServiceEntry(int entryId)
    {
        if (TryGetMarketServer(out var marketServer))
        {
            return marketServer.MarketServiceEntries.Remove(entryId);
        }
        return false;
    }
}
