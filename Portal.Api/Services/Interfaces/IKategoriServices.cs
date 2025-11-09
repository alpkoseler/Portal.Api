using Portal.Api.Models;

namespace Portal.Api.Services.Interfaces
{
    public interface IKategoriServices
    {
        Task<Kategori?> GetByIdAsync(int id);
        Task<List<Kategori>> GetAllAsync();
        Task<Kategori> AddAsync(string tanim);
        Task<Kategori?> UpdateAsync(int id, string tanim);
        Task<bool> DeleteAsync(int id);
    }
}
