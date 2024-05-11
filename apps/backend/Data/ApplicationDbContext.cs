using Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<List> Lists { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasPostgresEnum<PriorityEnum>();
    }
  }
}