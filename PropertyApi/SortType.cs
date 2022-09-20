namespace PropertyApi;

public class SortType
{
    public SearchSortType Type { get; init; } = SearchSortType.Newest;

    public override string ToString() => Type.ToString();

    public static bool TryParse(string value, out SortType output)
    {

        if (Enum.TryParse(typeof(SearchSortType), value, ignoreCase: true, out var searchSortType))
        {
            output = new SortType()
            {
                Type = (SearchSortType)searchSortType!
            };
            return true;
        }

        output = new SortType()
        {
            Type = SearchSortType.Newest
        };
        return true;
    }
}

public enum SearchSortType
{
    Newest,
    NetYieldDesc,
    PriceDesc,
    PriceAsc
}