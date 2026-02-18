using Microsoft.EntityFrameworkCore;
using V1.Web.Models;

namespace V1.Web.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PageContent> PageContents => Set<PageContent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PageContent>()
            .HasIndex(x => x.Slug)
            .IsUnique();
    }
}
