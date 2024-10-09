using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeasonalApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ProduceController : ControllerBase
{
    private readonly SeasonalDbContext _context;

    public ProduceController(SeasonalDbContext context)
    {
        _context = context;
    }

    // GET: api/produce
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produce>>> GetProduces()
    {
        return await _context.Produces
                             .Include(p => p.Seasons)
                             .ToListAsync();
    }

    // GET: api/produce/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Produce>> GetProduce(int id)
    {
        var produce = await _context.Produces
                                    .Include(p => p.Seasons)
                                    .FirstOrDefaultAsync(p => p.ProduceId == id);

        if (produce == null)
        {
            return NotFound();
        }
 Console.WriteLine($"Number of produces found: {_context.Produces.ToList().Count}"); // Add a console log here
        return produce;
    }
}
