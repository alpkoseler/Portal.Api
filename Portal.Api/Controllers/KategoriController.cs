using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models.Requests;
using Portal.Api.Models.Responses;
using Portal.Api.Services.Implementations;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriServices _kategoriService;
        private readonly IMapper _mapper;

        public KategoriController(IKategoriServices kategoriService, IMapper mapper)
        {
            _kategoriService = kategoriService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _kategoriService.GetAllAsync();
            return Ok(_mapper.Map<List<KategoriResponse>>(list));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var kategori = await _kategoriService.GetByIdAsync(id);

            if (kategori == null)
                return NotFound(new { message = "Kategori bulunamadı." });

            return Ok(_mapper.Map<KategoriResponse>(kategori));
        }

        [HttpPost]
        public async Task<IActionResult> Create(KategoriEkleRequest request)
        {
            var entity = await _kategoriService.AddAsync(request.Tanim);

            var response = _mapper.Map<KategoriResponse>(entity);

            return CreatedAtAction(nameof(Get), new { id = response.KategoriId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KategoriGuncelleRequest request)
        {
            var updated = await _kategoriService.UpdateAsync(id, request.Tanim);

            if (updated == null)
                return NotFound(new { message = "Kategori bulunamadı." });

            return Ok(_mapper.Map<KategoriResponse>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _kategoriService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Kategori bulunamadı." });

            return Ok(new { message = "Kategori silindi." });
        }
    }
}
