using Microsoft.EntityFrameworkCore;
using Portal.Api.Models;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Services.Implementations
{
    public class KategoriService : IKategoriServices
    {
        private readonly AppDbContext _ctx;

        public KategoriService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Kategori?> GetByIdAsync(int id)
        {
            return await _ctx.Kategoris.FindAsync(id);
        }

        public async Task<List<Kategori>> GetAllAsync()
        {
            return await _ctx.Kategoris
                .OrderBy(x => x.Tanim)
                .ToListAsync();
        }

        public async Task<Kategori> AddAsync(string tanim)
        {
            var entity = new Kategori
            {
                Tanim = tanim.Trim()
            };

            _ctx.Kategoris.Add(entity);
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task<Kategori?> UpdateAsync(int id, string tanim)
        {
            var entity = await _ctx.Kategoris.FindAsync(id);
            if (entity == null)
                return null;

            entity.Tanim = tanim.Trim();
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Kategoris.FindAsync(id);
            if (entity == null)
                return false;

            _ctx.Kategoris.Remove(entity);
            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
