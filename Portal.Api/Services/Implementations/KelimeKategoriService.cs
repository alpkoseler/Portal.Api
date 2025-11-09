using Microsoft.EntityFrameworkCore;
using Portal.Api.Models;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Services.Implementations
{
    public class KelimeKategoriService : IKelimeKategoriService
    {
        private readonly AppDbContext _ctx;

        public KelimeKategoriService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AddRelationAsync(int kelimeId, int kategoriId)
        {
            bool exists = await _ctx.KelimeKategoris
                .AnyAsync(x => x.KelimeId == kelimeId && x.KategoriId == kategoriId);

            if (exists)
                return false;

            _ctx.KelimeKategoris.Add(new KelimeKategori
            {
                KelimeId = kelimeId,
                KategoriId = kategoriId
            });

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRelationAsync(int kelimeId, int kategoriId)
        {
            var entity = await _ctx.KelimeKategoris
                .FirstOrDefaultAsync(x => x.KelimeId == kelimeId && x.KategoriId == kategoriId);

            if (entity == null)
                return false;

            _ctx.KelimeKategoris.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<List<Kategori>> GetCategoriesByKelimeIdAsync(int kelimeId)
        {
            return await _ctx.KelimeKategoris
                .Where(x => x.KelimeId == kelimeId)
                .Include(x => x.Kategori)
                .Select(x => x.Kategori)
                .ToListAsync();
        }
    }

}
