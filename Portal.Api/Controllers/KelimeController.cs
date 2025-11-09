using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models.Requests;
using Portal.Api.Models.Responses;
using Portal.Api.Services.Interfaces;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KelimeController : ControllerBase
    {
        private readonly IKelimeService _kelimeService;
        private readonly IMapper _mapper;

        public KelimeController(IKelimeService kelimeService, IMapper mapper)
        {
            _kelimeService = kelimeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kelimeler = await _kelimeService.GetAllAsync();
            var response = _mapper.Map<List<KelimeResponse>>(kelimeler);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var kelime = await _kelimeService.GetByIdAsync(id);

            if (kelime == null)
                return NotFound(new { message = "Kelime bulunamadı." });

            var response = _mapper.Map<KelimeResponse>(kelime);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KelimeEkleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _kelimeService.AddAsync(request.Tanim);
            var response = _mapper.Map<KelimeResponse>(entity);

            return CreatedAtAction(nameof(Get), new { id = response.KelimeId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KelimeGuncelleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _kelimeService.UpdateAsync(id, request.Tanim);

            if (updated == null)
                return NotFound(new { message = "Kelime bulunamadı." });

            var response = _mapper.Map<KelimeResponse>(updated);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _kelimeService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Kelime bulunamadı." });

            return Ok(new { message = "Kelime silindi." });
        }
    }
}
