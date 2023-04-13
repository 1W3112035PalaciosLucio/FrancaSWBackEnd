namespace FrancaSW.Services.Security
{
    public interface ISecurityService
    {
        string? GetUserName();
        bool CheckUserHasroles(string[] roles);
    }
}
