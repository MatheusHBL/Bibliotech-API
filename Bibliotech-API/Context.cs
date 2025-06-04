using Bibliotech_API.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<AutorEntity> Autor { get; set; }
}