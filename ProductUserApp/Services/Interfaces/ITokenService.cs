using System.Threading.Tasks;

namespace ProductUserApp.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task SaveTokenAsync(string token);
        Task ClearTokenAsync();
    }
}
