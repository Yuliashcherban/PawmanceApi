using Microsoft.EntityFrameworkCore;
using Pawmence.API.Entities;

namespace Pawmence.API.Data;

using Microsoft.EntityFrameworkCore;
using Pawmence.API.Entities;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<DogProfile> DogProfiles { get; set; } = null!;


    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!; 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<DogProfile>()
            .HasOne(dp => dp.User)
            .WithMany(u => u.DogProfiles)
            .HasForeignKey(dp => dp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Like>(e =>
        {
            e.HasKey(x => new { x.FromProfileId, x.ToProfileId });
            e.HasIndex(x => new { x.ToProfileId, x.FromProfileId });
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });

        
        modelBuilder.Entity<Match>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
            
            e.Property<Guid>("least_id")
             .HasComputedColumnSql("LEAST(profile_a, profile_b)", stored: true);
            e.Property<Guid>("greatest_id")
             .HasComputedColumnSql("GREATEST(profile_a, profile_b)", stored: true);

            e.HasIndex("least_id", "greatest_id")
             .IsUnique()
             .HasDatabaseName("match_pair_unique");
        });

       
        modelBuilder.Entity<Chat>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
            e.HasIndex(x => x.MatchId).IsUnique();
        });
        
        modelBuilder.Entity<User>()
            .Property(x => x.Id)
            .HasDefaultValueSql("gen_random_uuid()"); 
        
    }
}
