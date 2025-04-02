using api_biblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_biblioteca;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<AutorEntity> Autor { get; set; }
}