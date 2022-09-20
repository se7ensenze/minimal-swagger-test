namespace PropertyApi;

public interface IUserService
{
    Task<Guid[]> GetUserGroups(Guid userId);
}