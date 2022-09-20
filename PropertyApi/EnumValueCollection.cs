public record struct EnumValueCollection<T> where T: struct, Enum
{
    public T[] Values { get; init; }

    public EnumValueCollection(T[] values)
    {
        Values = values;
    }
    
    public static bool TryParse(string value, out EnumValueCollection<T> output)
    {
        var hasInvalidValue = false;
        
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s =>
            {
                if (int.TryParse(s, out var num) &&
                    typeof(T).IsEnum &&
                    Enum.IsDefined(typeof(T), num))
                {
                    return (T?)(T)(object)num;
                }

                hasInvalidValue = true;
                return default;
            })
            .Where(s => s != null)
            .Select(s => (T)s!)
            .ToArray();
        
        if (hasInvalidValue)
        {
            output = new (Array.Empty<T>());
            return false;
        }

        output = new EnumValueCollection<T>(segments.ToArray());
        return true;
    }
}