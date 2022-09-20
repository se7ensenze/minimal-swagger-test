public record struct PagingData
{
    public int PageNo { get; }
    public int PageSize { get; }

    public PagingData(int pageNo, int pageSize)
    {
        PageNo = pageNo;
        PageSize = pageSize;
    }
    
}