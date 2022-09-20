record struct SearchPropertyViewModel
{
    public Guid PropertyId { get; init; }
    public Guid SymbolId { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public decimal EstimatedNetRentalYield { get; init; }
    public int Type { get; init; }
    public bool IsPrivateAsset { get; init; }
    public Guid GroupId { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}