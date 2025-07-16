using Bibliotech_API.Features.Autores;
using Bibliotech_API.Features.Categorias;
using Bibliotech_API.Features.Corredores;
using Bibliotech_API.Features.Emprestimos;
using Bibliotech_API.Features.Estantes;
using Bibliotech_API.Features.Exemplares;
using Bibliotech_API.Features.Livros;
using Bibliotech_API.Features.Perfis;
using Bibliotech_API.Features.Prateleiras;
using Bibliotech_API.Features.Reservas;
using Bibliotech_API.Features.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Autor> Autores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Corredor> Corredores { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Estante> Estantes { get; set; }
    public DbSet<Exemplar> Exemplares { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Prateleira> Prateleiras { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Exemplar>()
            .Property(e => e.Situacao)
            .HasConversion<string>();

        modelBuilder.Entity<Emprestimo>()
            .Property(e => e.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Reserva>()
            .Property(r => r.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Livro>()
            .HasOne(l => l.Prateleira)
            .WithMany(p => p.Livros)
            .HasForeignKey(l => l.IdPrateleira)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Exemplar>()
            .HasOne(e => e.Livro)
            .WithMany(l => l.Exemplares)
            .HasForeignKey(e => e.IdLivro)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Exemplar)
            .WithMany(ex => ex.Emprestimos)
            .HasForeignKey(e => e.IdExemplar)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.UsuarioLeitor)
            .WithMany(u => u.EmprestimosComoLeitor)
            .HasForeignKey(e => e.IdUsuarioLeitor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.UsuarioResponsavel)
            .WithMany(u => u.EmprestimosComoResponsavel)
            .HasForeignKey(e => e.IdUsuarioResponsavel)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Exemplar)
            .WithMany(ex => ex.Reservas)
            .HasForeignKey(r => r.IdExemplar)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Reservas)
            .HasForeignKey(r => r.IdUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prateleira>()
            .HasOne(p => p.Estante)
            .WithMany(e => e.Prateleiras)
            .HasForeignKey(p => p.IdEstante)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Estante>()
            .HasOne(e => e.Corredor)
            .WithMany(c => c.Estantes)
            .HasForeignKey(e => e.IdCorredor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Livro>()
            .HasMany(l => l.Autores)
            .WithMany(a => a.Livros)
            .UsingEntity(j => j.ToTable("livro_autor"));

        modelBuilder.Entity<Livro>()
            .HasMany(l => l.Categorias)
            .WithMany(c => c.Livros)
            .UsingEntity(j => j.ToTable("livro_categoria"));

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Perfis)
            .WithMany(p => p.Usuarios)
            .UsingEntity(j => j.ToTable("usuario_perfil"));
    }
}