using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Api.Models;

namespace Portal.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class KelimeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KelimeController(AppDbContext context)
        {
            _context = context;
        }

    }
}
