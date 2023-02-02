using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Seeds;
using FoodDelivery.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodDelivery.Api.DAL.EF;

public class FoodDeliveryDbContext : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    public DbSet<FoodEntity> Foods { get; set; } = null!;
    public DbSet<RestaurantEntity> Restaurants { get; set; } = null!;
    public DbSet<FoodOrderNoteEntity> FoodOrderNotes { get; set; } = null!;

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>()
            .HasMany(orderEntity => orderEntity.FoodOrderNotes)
            .WithOne(foodOrderNoteEntity => foodOrderNoteEntity.Order)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RestaurantEntity>()
            .HasMany(restaurantEntity => restaurantEntity.Orders)
            .WithOne(orderEntity => orderEntity.Restaurant)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RestaurantEntity>()
            .HasMany(restaurantEntity => restaurantEntity.Foods)
            .WithOne(foodEntity => foodEntity.Restaurant)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FoodEntity>()
            .HasMany(typeof(FoodOrderNoteEntity))
            .WithOne(nameof(FoodOrderNoteEntity.Food))
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FoodEntity>()
            .Property(e => e.Allergens)
            .HasConversion(
        v => string.Join(",", v.Select(a => a.ToString())),
        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(a => (Allergen)Enum.Parse(typeof(Allergen), a)).ToList(),
        new ValueComparer<ICollection<Allergen>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()));

            FoodOrderNotesSeeds.Seed(modelBuilder);
            OrderSeeds.Seed(modelBuilder);
            FoodSeeds.Seed(modelBuilder);
            RestaurantSeeds.Seed(modelBuilder);
    }
}
