using System.Net;

namespace PropertyApi;

public record struct SearchFilter
{
    public PropertyType[] PropertyTypeFilters { get; init; }
    public SymbolMarketStatus[] PropertyStatusFilters { get; init; }
    public bool ShowOnlyIAmMeberOfGroup { get; init; }
    private SearchFilter(
        PropertyType[] propertyTypeFilters, 
        SymbolMarketStatus[] propertyStatusFilters,
        bool showOnlyIAmMeberOfGroup)
    {
        PropertyTypeFilters = propertyTypeFilters;
        PropertyStatusFilters = propertyStatusFilters;
        ShowOnlyIAmMeberOfGroup = showOnlyIAmMeberOfGroup;
    }

    public static bool TryParse(string value, out SearchFilter output)
    {
        var containsInvalidValue = false;
        
        var propertyTypeFilters = new List<PropertyType>();
        var propertyStatusFilters = new List<SymbolMarketStatus>();
        var showOnlyIAmMeberOfGroup = false;
        
        value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(s => !string.IsNullOrEmpty(s))
            .ToList()
            .ForEach(s =>
            {
                if ("ismember".Equals(s, StringComparison.InvariantCultureIgnoreCase))
                {
                    showOnlyIAmMeberOfGroup = true;
                }
                else if (Enum.TryParse(typeof(PropertyType), s, ignoreCase: true, out var propertyType))
                {
                    propertyTypeFilters.Add((PropertyType)propertyType!);
                }
                else if (Enum.TryParse(typeof(SymbolMarketStatus), s, ignoreCase: true, out var propertyStatus))
                {
                    propertyStatusFilters.Add((SymbolMarketStatus)propertyStatus!);
                }
                else
                {
                    containsInvalidValue = true;
                }
            });

        if (containsInvalidValue)
        {
            output = new SearchFilter();
            return false;
        }

        output = new SearchFilter(propertyTypeFilters.ToArray(), propertyStatusFilters.ToArray(), showOnlyIAmMeberOfGroup);
        return true;
    }
}

