public record struct PropertyTypeSearhFilter
{
    public PropertyType[] Values { get; init; }
    
    public static PropertyTypeSearhFilter Empty = new PropertyTypeSearhFilter(Array.Empty<PropertyType>());
    
    public PropertyTypeSearhFilter(PropertyType[] values)
    {
        Values = values;
    }
    
    public static bool TryParse(string value, out PropertyTypeSearhFilter output)
    {
        var hasInvalidValue = false;
        
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s =>
            {
                if (int.TryParse(s, out var num) &&
                    Enum.IsDefined(typeof(PropertyType), num))
                {
                    return (PropertyType?)num;
                }

                hasInvalidValue = true;
                return null;
            })
            .Where(s => s != null)
            .Select(s => s!.Value)
            .ToArray();
        
        if (hasInvalidValue)
        {
            output = PropertyTypeSearhFilter.Empty;
            return false;
        }

        output = new PropertyTypeSearhFilter(segments.ToArray());
        return true;
    }
}