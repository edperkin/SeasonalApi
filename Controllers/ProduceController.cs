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

        return produce;
    }

    [HttpGet("week/{weekNumber}")]
    public async Task<ActionResult<IEnumerable<Season>>> GetProduceByWeek(int weekNumber, [FromQuery] ProduceType produceType)
    {
        var produceByWeek = await _context.Seasons
                                    .Include(s => s.Produce)
                                    .Where(s => s.WeekOfYear == weekNumber && s.Produce != null && s.Produce.ProduceType == produceType)
                                    .ToListAsync();
        if (produceByWeek == null)
        {
            return NotFound();
        }
        
        return produceByWeek;
    }
}
