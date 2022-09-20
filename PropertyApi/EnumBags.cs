namespace PropertyApi;

public record struct EnumBags
{
    private readonly string[] _values = Array.Empty<string>();
    
    private EnumBags(string[] values)
    {
        _values = values;
    }

    public T[] GetValues<T>() where T : Enum
    {
        var enumValueList = new List<T>();
        foreach (var value in _values)
        {
            if (Enum.TryParse(typeof(T), value, ignoreCase: true, out var enumValue))
            {
                enumValueList.Add((T)enumValue!);
            }
        }

        return enumValueList.ToArray();

    }

    public EnumBags()
    {
        _values = Array.Empty<string>();
    }

    public static bool TryParse(string value, out EnumBags output)
    {
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(s => !string.IsNullOrEmpty(s))
            .ToArray();

        output = new EnumBags(segments);
        return true;
    }
}