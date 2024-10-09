using Microsoft.EntityFrameworkCore;
using SeasonalApi.Models;

public class SeasonalDbContext(DbContextOptions<SeasonalDbContext> options) : DbContext(options)
{
    public DbSet<Produce> Produces { get; set; } = null!;
    public DbSet<Season> Seasons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Season>().HasOne(s => s.Produce)
            .WithMany(p => p.Seasons).HasForeignKey(s => s.ProduceId);

            modelBuilder.Entity<Produce>().HasData(
                new Produce(1, "Apple", ProduceType.Fruit),
                new Produce(2, "Carrot", ProduceType.Vegetable),
                new Produce(3, "Strawberry", ProduceType.Fruit)
            );

            modelBuilder.Entity<Season>().HasData(
                new Season(1, 10, 1), 
                new Season(2, 11, 1), 
                new Season(3, 10, 2), 
                new Season(4, 11, 2), 
                new Season(5, 22, 3),
                new Season(6, 23, 3) 
            );
    }
}