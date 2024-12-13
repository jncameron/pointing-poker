using Microsoft.EntityFrameworkCore;
using PointingPoker.Models;

namespace PointingPoker.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Session> Sessions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  }
}
