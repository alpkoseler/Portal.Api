using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Api.Models;

namespace Portal.Api.Controllers
{
    [Route("api/kelime")]
    [ApiController]
    public class KelimeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KelimeController(AppDbContext context)
        {
            _context = context;
        }
        #region UcHarf
        // GET api/kelime/liste
        [HttpGet("liste")]
        public async Task<IActionResult> GetAll()
        {
            var kelimeler = await _context.UcHarfKelimes.ToListAsync();
            return Ok(kelimeler);
        }

        // GET api/kelime/detay/5
        [HttpGet("detay/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kelime = await _context.UcHarfKelimes.FindAsync(id);
            if (kelime == null)
                return NotFound(new { message = "Kelime bulunamadı." });

            return Ok(kelime);
        }

        // POST api/kelime/ekle
        [HttpPost("ekle")]
        public async Task<IActionResult> Create([FromBody] UcHarfKelime model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.UcHarfKelimes.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.UcHarfKelimeId }, model);
        }

        // DELETE api/kelime/sil/5
        [HttpDelete("sil/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var kelime = await _context.UcHarfKelimes.FindAsync(id);
            if (kelime == null)
                return NotFound(new { message = "Silinecek kelime bulunamadı." });

            _context.UcHarfKelimes.Remove(kelime);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kelime başarıyla silindi." });
        }
        #endregion

    }
}
