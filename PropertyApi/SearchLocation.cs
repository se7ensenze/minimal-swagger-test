public record struct SearchLocation(decimal Latitude, decimal Longitude, decimal Radius)
{
    public static SearchLocation Empty = new SearchLocation(0, 0, 0);
    
    public override string ToString() => $"({Latitude},{Longitude},{Radius})";

    public static bool TryParse(string value, out SearchLocation output)
    { 
        var trimmedValue = value?.TrimStart('(').TrimEnd(')');
        var segments = trimmedValue?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments?.Length == 3
            && decimal.TryParse(segments[0], out var latitude)
            && decimal.TryParse(segments[1], out var longitude)
            && decimal.TryParse(segments[2], out var radius))
        {
            output = new SearchLocation(latitude, longitude, radius);
            return true;
        }

        output = SearchLocation.Empty;
        return false;
    }
}