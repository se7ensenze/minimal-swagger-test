using System.Reflection;
using PropertyApi;

public record struct SearchPropertyV3Request
{
    public string SearchText { get; init; }
    public SearchLocation Location { get; init; }
    public StringSearchFilter PropertyStatusFilter { get; init; }
    public StringSearchFilter MemberTypeFilter { get; init; }
    public PropertyTypeSearhFilter PropertyTypeFilter { get; init; }
    public PagingData PagingData { get; init; }
    public string SortBy { get; init; }
    public Guid CurrentUserId { get; init; }
    
    public static ValueTask<SearchPropertyV3Request?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        const string searchTextKey = "searchtext";
        const string locationKey = "location";
        const string propertyStatusFilterKey = "filters.propertystatus";
        const string memberTypeFilterKey = "filters.membertype";
        const string propertyTypeFilterKey = "filters.propertytype";
        const string pageNoKey = "pageno";
        const string pageSizeKey = "pagesize";
        const string sortByKey = "sort";
        
        string? searchText = context.Request.Query[searchTextKey];
        SearchLocation.TryParse(context.Request.Query[locationKey], out var location);
        StringSearchFilter.TryParse(context.Request.Query[propertyStatusFilterKey], out var propertyStatusFilter);
        StringSearchFilter.TryParse(context.Request.Query[memberTypeFilterKey], out var memberTypeFilter);
        PropertyTypeSearhFilter.TryParse(context.Request.Query[propertyTypeFilterKey], out var propertyTypeFilter);
        int.TryParse(context.Request.Query[pageNoKey], out var pageNo);
        int.TryParse(context.Request.Query[pageSizeKey], out var pageSize);
        
        return ValueTask.FromResult<SearchPropertyV3Request?>(
            new SearchPropertyV3Request
            {
                SearchText = searchText,
                Location = location,
                PropertyStatusFilter = propertyStatusFilter,
                MemberTypeFilter = memberTypeFilter,
                PropertyTypeFilter = propertyTypeFilter,
                PagingData = new PagingData(pageNo, pageSize),
                CurrentUserId = context.GetCurrentUserId(),
                SortBy = sortByKey ?? string.Empty
            });
    }
}