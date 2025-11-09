using Portal.Api.Models;

namespace Portal.Api.Services.Interfaces
{
    public interface IKelimeService
    {
        Task<Kelime?> GetByIdAsync(int id);
        Task<List<Kelime>> GetAllAsync();
        Task<Kelime> AddAsync(string tanim);
        Task<Kelime?> UpdateAsync(int id, string tanim);
        Task<bool> DeleteAsync(int id);
    }
}
