using Microsoft.EntityFrameworkCore;
using Portal.Api.Models;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Services.Implementations
{
    public class KelimeService : IKelimeService
    {
        private readonly AppDbContext _ctx;

        public KelimeService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Kelime?> GetByIdAsync(int id)
        {
            return await _ctx.Kelimes.FindAsync(id);
        }

        public async Task<List<Kelime>> GetAllAsync()
        {
            return await _ctx.Kelimes
                .OrderBy(k => k.Tanim)
                .ToListAsync();
        }

        public async Task<Kelime> AddAsync(string tanim)
        {
            var entity = new Kelime
            {
                Tanim = tanim.Trim()
            };

            _ctx.Kelimes.Add(entity);
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task<Kelime?> UpdateAsync(int id, string tanim)
        {
            var entity = await _ctx.Kelimes.FindAsync(id);

            if (entity == null)
                return null;

            entity.Tanim = tanim.Trim();

            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Kelimes.FindAsync(id);
            if (entity == null)
                return false;

            _ctx.Kelimes.Remove(entity);
            await _ctx.SaveChangesAsync();

            return true;
        }
    }

}
