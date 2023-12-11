using Microsoft.EntityFrameworkCore;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Form> Forms { get; set; }
}