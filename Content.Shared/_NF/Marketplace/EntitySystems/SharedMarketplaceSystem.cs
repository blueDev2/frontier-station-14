using Content.Shared.GameTicking;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._NF.Marketplace.EntitySystems;

public enum MarketAgentType : byte
{
    Buyer,
    Seller
}


public enum MarketEntryType : byte
{
    Good,
    Service
}


public enum MarketGoodEntryCategory : byte
{
    FoodsAndDrinks,
    ResearchAndDevelopment,
    EngineeringAndAtmospherics,
    ChemicalsAndMedicine,
    BasicMats,
    Misc
}


public enum MarketServiceEntryCategory : byte
{
    Employment,
    Construction,
    Janitorial,
    Misc
}

public sealed class Agent
{
    public MarketAgentType AgentType;
    public string Name;
    public Agent(string name, MarketAgentType agentType)
    {
        Name = name;
        AgentType = agentType;
    }
}

public sealed class MarketGoodEntry
{
    /// <summary>
    /// A buyer or seller of goods
    /// </summary>
    public Agent Agent;

    /// <summary>
    /// Value of the goods according to the Agent
    /// </summary>
    public int Price;

    /// <summary>
    /// Miscellaneous information about the goods that the agent believes is important for the deal
    /// </summary>
    public string Description;

    /// <summary>
    /// The Vessel that the agent has goods, or wants goods delivered
    /// </summary>
    public string AssociatedVessel;

    /// <summary>
    /// The categories of the goods being sold
    /// </summary>
    public List<MarketGoodEntryCategory> GoodsCategories;

    /// <summary>
    /// The goods listed for the entry
    /// </summary>
    public List<EntityPrototype> Goods;

    /// <summary>
    /// How many of these goods sets are remaining available for sale or being requested
    /// </summary>
    public int Quantity;

    public MarketGoodEntry(Agent agent, string associatedVessel, int price, string description, int quantity, List<MarketGoodEntryCategory> goodsCategories, List<EntityPrototype> goods)
    {
        Agent = agent;
        Price = price;
        Description = description;
        AssociatedVessel = associatedVessel;
        GoodsCategories = goodsCategories;
        Goods = goods;
        Quantity = quantity;
    }
}

public sealed class MarketServiceEntry
{
    /// <summary>
    /// A buyer or seller of a service
    /// </summary>
    public Agent Agent;

    /// <summary>
    /// Value of the service according to the Agent
    /// </summary>
    public int Price;
    /// <summary>
    /// Description of the requested service
    /// </summary>
    public string Description;

    /// <summary>
    /// The Vessel that the agent is at, or wants the service at
    /// </summary>
    public string AssociatedVessel;

    /// <summary>
    /// The categories of the service being sold
    /// </summary>
    public List<MarketServiceEntryCategory> ServiceCategories;

    public MarketServiceEntry(Agent agent, string associatedVessel, int price, string description, List<MarketServiceEntryCategory> serviceCategories)
    {
        Agent = agent;
        Price = price;
        Description = description;
        AssociatedVessel = associatedVessel;
        ServiceCategories = serviceCategories;
    }
}

public abstract class SharedMarketplaceSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        //SubscribeLocalEvent<RoundStartedEvent>();

        // UI Event Handlers

    }
}
