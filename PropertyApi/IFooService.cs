namespace PropertyApi;

public interface IFooService
{ 
    Task<IEnumerable<FooData>> GetFooData(GetFooDataRequest request);
}

public class GetFooDataRequest
{
    
}

public class FooData
{
    public string SymbolName { get; set; }
    public SymbolMarketStatus MarketStatus { get; set; }
}

public interface IFighterService
{
    Task<IEnumerable<FighterData>> GetFighterData(GetFighterDataRequest request);
}

public class GetFighterDataRequest
{
    public string SearchText { get; set; }
    public SearchLocation? SearchLocation { get; set; }
    public PropertyType[] PropertyTypeFilter { get; set; }
    public bool IncludPrivateGroup { get; set; }
    public Guid[] GroupIdFilters { get; set; }
}

public class FighterData
{
}