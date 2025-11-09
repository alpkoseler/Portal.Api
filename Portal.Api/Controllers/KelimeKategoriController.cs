using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Portal.Api.Models.Responses;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KelimeKategoriController : ControllerBase
    {
        private readonly IKelimeKategoriService _kelimeKategoriService;
        private readonly IMapper _mapper;

        public KelimeKategoriController(IKelimeKategoriService kelimeKategoriService, IMapper mapper)
        {
            _kelimeKategoriService = kelimeKategoriService;
            _mapper = mapper;
        }

        [HttpPost("{kelimeId}/kategori/{kategoriId}")]
        public async Task<IActionResult> AddRelation(int kelimeId, int kategoriId)
        {
            bool added = await _kelimeKategoriService.AddRelationAsync(kelimeId, kategoriId);

            if (!added)
                return Conflict(new { message = "Kategori zaten bu kelimeye bağlı." });

            return Ok(new { message = "Kategori başarılı şekilde bağlandı." });
        }

        [HttpGet("{kelimeId}/kategoriler")]
        public async Task<IActionResult> GetCategories(int kelimeId)
        {
            var categories = await _kelimeKategoriService.GetCategoriesByKelimeIdAsync(kelimeId);

            var response = _mapper.Map<List<KategoriResponse>>(categories);

            return Ok(response);
        }

        [HttpDelete("{kelimeId}/kategori/{kategoriId}")]
        public async Task<IActionResult> RemoveRelation(int kelimeId, int kategoriId)
        {
            bool removed = await _kelimeKategoriService.RemoveRelationAsync(kelimeId, kategoriId);

            if (!removed)
                return NotFound(new { message = "Bu ilişki bulunamadı." });

            return Ok(new { message = "Kategori kelimeden kaldırıldı." });
        }
    }
}
