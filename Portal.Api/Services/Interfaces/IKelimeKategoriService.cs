using Portal.Api.Models;

namespace Portal.Api.Services.Interfaces
{
    public interface IKelimeKategoriService
    {
        Task<bool> AddRelationAsync(int kelimeId, int kategoriId);
        Task<bool> RemoveRelationAsync(int kelimeId, int kategoriId);
        Task<List<Kategori>> GetCategoriesByKelimeIdAsync(int kelimeId);
    }
}
