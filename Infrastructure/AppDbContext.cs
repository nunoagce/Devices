using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Device> Devices => Set<Device>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(builder =>
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedNever();

            builder.Property(d => d.Name)
                .IsRequired();

            builder.Property(d => d.Brand)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(d => d.CreationTime)
                .IsRequired();

            builder.Property(d => d.State)
                .HasConversion<string>();
        });
    }
}