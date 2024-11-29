namespace ProductUserApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> IsInRoleAsync(string role);
        Task LogoutAsync();
        Task<string> GetCurrentUserRoleAsync();
    }
}
