public record struct SearchPropertyV1Request
{
    public string SearchText { get; init; }
    public SearchLocation Location { get; init; }
    public StringSearchFilter PropertyStatusFilter { get; init; }
    public StringSearchFilter MemberTypeFilter { get; init; }
    public PropertyTypeSearhFilter PropertyTypeFilter { get; init; }
    public PagingData PagingData { get; init; } 
    public string SortBy { get; init; }
    public Guid CurrentUserId { get; init; }
    public SearchPropertyV1Request(string? searchText, string? propertyTypeFilters, string? propertyStatusFilters,
        decimal? latitude, decimal? longitude, decimal? radius,
        string? memberTypeFilters, Guid currentUserId, int? pageNo, int? pageSize, string? sortBy)
    {
        
        SearchLocation.TryParse($"({latitude},{longitude},{radius})", out var location);
        StringSearchFilter.TryParse(propertyStatusFilters ?? string.Empty, out var propertyStatusFilter);
        StringSearchFilter.TryParse(memberTypeFilters ?? string.Empty, out var memberTypeFilter);
        PropertyTypeSearhFilter.TryParse(propertyTypeFilters ?? string.Empty, out var propertyTypeFilter);

        SearchText = searchText ?? string.Empty;
        Location = location;
        PropertyStatusFilter = propertyStatusFilter;
        MemberTypeFilter = memberTypeFilter;
        PropertyTypeFilter = propertyTypeFilter;
        PagingData = new PagingData(pageNo ?? 0, pageSize ?? 0);
        SortBy = sortBy ?? string.Empty;
        CurrentUserId = currentUserId;
    }

}