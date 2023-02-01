using Microsoft.EntityFrameworkCore;

namespace anki.Domain;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardTag>()
        .HasKey(t => new { t.CardId, t.TagId });
    }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<CardTag> CardTags { get; set; }
}