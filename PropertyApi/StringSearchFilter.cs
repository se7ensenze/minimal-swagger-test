public record struct StringSearchFilter
{
    public StringSearchFilter(string[] values)
    {
        Values = values;
    }

    public string[] Values { get; init; }
    
    public static StringSearchFilter InvalidFilter = new StringSearchFilter(Array.Empty<string>());
    
    public static bool TryParse(string value, out StringSearchFilter output)
    {
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(s => !string.IsNullOrEmpty(s))
            .ToArray();

        if (segments.Length == 0)
        {
            output = StringSearchFilter.InvalidFilter;
            return false;
        }

        output = new StringSearchFilter(segments);
        return true;
    }

}