using System.Threading.Tasks;

namespace XamarinChallenge.Services.Interfaces
{
    public interface IClientFactory
    {
        Task<TResponse> GetAsync<TResponse>(string url);
    }
}
