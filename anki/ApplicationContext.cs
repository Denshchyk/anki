using Anki;
using Microsoft.EntityFrameworkCore;

namespace anki;

public class ApplicationContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=anki;Username=myusername;Password=mypassword");
    }
}