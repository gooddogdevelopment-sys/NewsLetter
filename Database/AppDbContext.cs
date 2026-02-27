using Microsoft.EntityFrameworkCore;
using NewsLetter.Models;

namespace NewsLetter.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Newsletter> NewsLetters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Newsletter>().HasIndex(x => x.AiProvider);
    }
}