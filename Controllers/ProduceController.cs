using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeasonalApi.DTOs;
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
    public async Task<ActionResult<IEnumerable<ProduceDto>>> GetProduceByWeek(int weekNumber, [FromQuery] ProduceType produceType)
    {
        var produceByWeek = await _context.Seasons
                                    .Include(s => s.Produce)
                                    .Where(s => s.WeekOfYear == weekNumber && s.Produce != null && s.Produce.ProduceType == produceType)
                                    .ToListAsync();
        if (produceByWeek == null)
        {
            return NotFound();
        }

        var weeksRemainingCount = 0;
        
        foreach (var produce in produceByWeek)
        {
            var seasonList =  await _context.Seasons.Where(s => s.ProduceId == produce.ProduceId).ToListAsync();

            weeksRemainingCount += seasonList.Count(season => season.WeekOfYear > weekNumber);
        }

        return produceByWeek.Select(produce => new ProduceDto(produce.Produce.Name, produce.Produce.ImageUrl, produce.Produce.Colour, weeksRemainingCount)).ToList();
    }
}
