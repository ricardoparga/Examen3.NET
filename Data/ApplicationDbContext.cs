using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen3.NET.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Comida> Comidas { get; set; }
    public DbSet<Bebida> Bebidas { get; set; }
    
}
