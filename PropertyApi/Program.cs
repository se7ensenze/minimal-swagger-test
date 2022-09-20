// using System.Runtime.InteropServices.ComTypes;
// using System.Text.Json.Serialization;

using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic.CompilerServices;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Diagnostics;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Npgsql;
using PropertyApi;
// using SqlKata.Compilers;
// using SqlKata.Execution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/properties/v1/search",  object (
    string? searchText,
    decimal? latitude,
    decimal? longitude,
    decimal? radius,
    string? propertyTypeFilters,
    string? propertyStatusFilters,
    string? memberTypesFilters,
    int? pageNo,
    int? pageSize,
    string? sortBy,
    HttpContext context) =>
    {
        var request = new SearchPropertyV1Request(searchText, 
            propertyTypeFilters, 
            propertyStatusFilters, 
            latitude, 
            longitude, 
            radius, 
            memberTypesFilters, 
            context.GetCurrentUserId(),
            pageNo,
            pageSize,
            sortBy);

        return TypedResults.Ok(new
        {
            searchText = request.SearchText,
            location = request.Location,
            propertyStatusFilter = request.PropertyStatusFilter.Values,
            memberTypeFilter = request.MemberTypeFilter.Values,
            propertyTypeFilter = request.PropertyTypeFilter.Values,
            pageNo = request.PagingData.PageNo,
            pageSize = request.PagingData.PageSize,
            sort = request.SortBy
        });
    })
.WithName("SearchV1")
.WithTags("Property");

app.MapGet("/properties/v2/search", object (
        string searchText,
        SearchLocation? location,
        EnumValueCollection<SymbolMarketStatus>? propertyStatusFilter,
        EnumValueCollection<MemberFilterTypes>? memberTypeFilter, 
        EnumValueCollection<PropertyType>? propertyTypeFilter,
        int? pageNo,
        int? pageSize,
        string? sort,
        ILoggerFactory loggerFactory,
        HttpContext context) =>
    {

        #region Thinking Area

        //FOR execution flow
        // PropertySearchingStrategy strategy = new PropertySearchingStrategy();
        //
        // IFooService fooService = default;
        //
        // IEnumerable<FooData> fooData = await fooService.GetFooData(new GetFooDataRequest());
        //
        // if (!string.IsNullOrEmpty(searchText))
        // {
        //     fooData = fooData.Where(f => f.SymbolName == searchText);
        // }
        //
        // if (propertyStatusFilter.HasValue && propertyStatusFilter.Value.Values.Length > 0)
        // {
        //     fooData = fooData.Where(f => propertyStatusFilter.Value.Values.Contains(f.MarketStatus));
        // }
        //
        // IFighterService fighterService = default;
        // Guid[] specificPropertyGroupIds = Array.Empty<Guid>();
        //
        // //public user will see only public property
        // bool includPrivateGroup = false;
        //
        // //if user is logged in
        // if (context.GetCurrentUserId() != Guid.Empty)
        // {
        //     includPrivateGroup = true;
        // }
        //
        // //filter only if the property that current is member
        // if (includPrivateGroup && memberTypeFilter.HasValue && memberTypeFilter.Value.Values.Length > 0 )
        // {
        //     //just for prototyping
        //     IUserService userService = default;
        //     specificPropertyGroupIds = await userService.GetUserGroups(context.GetCurrentUserId());
        // }
        //
        // IEnumerable<FighterData> fighterData = await fighterService.GetFighterData(new GetFighterDataRequest()
        // {
        //     SearchText = searchText,
        //     SearchLocation = location,
        //     PropertyTypeFilter = propertyTypeFilter?.Values ?? Array.Empty<PropertyType>(),
        //     IncludPrivateGroup = includPrivateGroup,
        //     GroupIdFilters = specificPropertyGroupIds
        // });

        //FOR CHECK SQL command output
        // bool noUserLogin = context.User.Identity == null;
        //
        // var includeHidden = noUserLogin && memberTypeFilter?.Values.Contains("private", StringComparer.InvariantCultureIgnoreCase) ?? true;
        //
        // await using var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=product;User Id=postgres;Password=postgres;");
        // var factory = new QueryFactory(connection, new PostgresCompiler());
        // var query = factory.Query("property_view")
        //     .Select("propertyid", "symbolid", "city", "country", "estimatednetrentalyield", "type")
        //     .Select("isprivateasset", "groupid", "latitude", "longitude")
        //     .WhenNot(includeHidden, i => i.Where("isvisibletopublic", true))
        //     .Where(i => i
        //         .WhereLike("title", searchText, caseSensitive: false)
        //         .OrWhereLike("description", searchText, caseSensitive: false)
        //         .OrWhereLike("city", searchText, caseSensitive: false)
        //         .OrWhereLike("street", searchText, caseSensitive: false));
        //
        // var sql = factory.Compiler.Compile(query).Sql;
        //
        // loggerFactory.CreateLogger("minimal")
        //     .LogInformation(sql);

        //var result = await query.GetAsync<SearchPropertyViewModel>();

        #endregion

        return TypedResults.Ok(new
        {
            searchText,
            location,
            propertyStatusFilter = propertyStatusFilter?.Values,
            memberTypeFilter = memberTypeFilter?.Values,
            propertyTypeFilter = propertyTypeFilter?.Values,
            pageNo,
            pageSize,
            sort
        });
    })
    .WithName("SearchV2")
    .WithTags("Property");

app.MapGet("/properties/v3/search",object (
        SearchPropertyV3Request request) => TypedResults.Ok(new
    {
        searchText = request.SearchText,
        location = request.Location,
        propertyStatusFilter = request.PropertyStatusFilter.Values,
        memberTypeFilter = request.MemberTypeFilter.Values,
        propertyTypeFilter = request.PropertyTypeFilter.Values,
        pageNo = request.PagingData.PageNo,
        pageSize = request.PagingData.PageSize,
        sort = request.SortBy
    }))
    .WithName("SearchV3")
    .WithTags("Property");

app.MapGet("/properties/v4/search",object (
        string? searchText,
        SearchLocation? location,
        SearchFilter? filters,
        int? pageNo,
        int? pageSize,
        SortType? sort,
        HttpContext context) => TypedResults.Ok(new
    {
        searchText = searchText ?? string.Empty,
        location = location,
        propertyStatusFilter = filters?.PropertyTypeFilters,
        memberTypeFilter = filters?.ShowOnlyIAmMeberOfGroup,
        propertyTypeFilter = filters?.PropertyTypeFilters,
        pageNo = pageNo,
        pageSize = pageSize,
        sort = sort?.ToString()
    }))
    .WithName("SearchV4")
    .WithTags("Property");

app.Run();