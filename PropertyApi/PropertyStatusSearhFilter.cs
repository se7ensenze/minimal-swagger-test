public record struct PropertyStatusSearhFilter
{
    public SymbolMarketStatus[] Values { get; init; }
    
    public static PropertyStatusSearhFilter Empty = new PropertyStatusSearhFilter(Array.Empty<SymbolMarketStatus>());
    
    public PropertyStatusSearhFilter(SymbolMarketStatus[] values)
    {
        Values = values;
    }
    
    public static bool TryParse(string value, out PropertyStatusSearhFilter output)
    {
        var hasInvalidValue = false;
        
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s =>
            {
                if (int.TryParse(s, out var num) &&
                    Enum.IsDefined(typeof(SymbolMarketStatus), num))
                {
                    return (SymbolMarketStatus?)num;
                }

                hasInvalidValue = true;
                return null;
            })
            .Where(s => s != null)
            .Select(s => s!.Value)
            .ToArray();
        
        if (hasInvalidValue)
        {
            output = PropertyStatusSearhFilter.Empty;
            return false;
        }

        output = new PropertyStatusSearhFilter(segments.ToArray());
        return true;
    }
}