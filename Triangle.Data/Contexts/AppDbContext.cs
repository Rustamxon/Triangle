using Microsoft.EntityFrameworkCore;
using Triangle.Domain.Entities;

namespace Triangle.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
}
