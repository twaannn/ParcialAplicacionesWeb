using Microsoft.EntityFrameworkCore;
using AplicacionesWeb.Models;

namespace AplicacionesWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Persona> Personas => Set<Persona>();
        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<CuentaBancaria> CuentasBancarias => Set<CuentaBancaria>();
    }
}
