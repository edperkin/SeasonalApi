namespace SeasonalApi.Models;

public record Produce(int ProduceId, string Name, ProduceType ProduceType)
    {
        public ICollection<Season> Seasons { get; init; } = new List<Season>();
    }

public enum ProduceType
{
    Fruit, 
    Vegetable
}

 public record Season(int SeasonId, int WeekOfYear, int ProduceId)
    {
        public Produce? Produce { get; init; }
    }