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
                new Season(1, 10, 1), // Apple is in season in week 10
                new Season(2, 11, 1), // Apple is in season in week 11
                new Season(3, 20, 2), // Carrot is in season in week 20
                new Season(4, 21, 2), // Carrot is in season in week 21
                new Season(5, 22, 3), // Strawberry is in season in week 22
                new Season(6, 23, 3)  // Strawberry is in season in week 23
            );


    }
}