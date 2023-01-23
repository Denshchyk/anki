using Microsoft.EntityFrameworkCore;

namespace anki.Domain;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<Card> Cards { get; set; }
}