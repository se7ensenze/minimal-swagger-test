namespace PropertyApi;

public static class HttpContextExtension
{
    public static Guid GetCurrentUserId(this HttpContext context)
    {
        return Guid.NewGuid();
    }
}