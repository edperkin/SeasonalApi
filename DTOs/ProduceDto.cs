using SeasonalApi.Models;

namespace SeasonalApi.DTOs
{
    public record ProduceDto(string Name, string ImageUrl, string Colour, int WeeksRemaining);
}