using Bibliotech_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Data;

public class BibliotechContext : DbContext
{
    public BibliotechContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<AutorEntity> Autor { get; set; }
}