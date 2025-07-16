using Bibliotech_API.Data;
using Bibliotech_API.Features.Autores;
using Bibliotech_API.Features.Categorias;
using Bibliotech_API.Features.Corredores;
using Bibliotech_API.Features.Estantes;
using Bibliotech_API.Features.Exemplares;
using Bibliotech_API.Features.Livros;
using Bibliotech_API.Features.Perfis;
using Bibliotech_API.Features.Prateleiras;
using Bibliotech_API.Features.Reservas;
using Bibliotech_API.Features.Usuarios;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(ServerVersion.AutoDetect(connection)))
        .UseSnakeCaseNamingConvention()
);

builder.Services.AddControllers();

builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IExemplarService, ExemplarService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IPrateleiraService, PrateleiraService>();
builder.Services.AddScoped<IEstanteService, EstanteService>();
builder.Services.AddScoped<ICorredorService, CorredorService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();