using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Library.Enums;

namespace LibreriaVirtualData.Library.Context
{
    public class LibreriaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }

        public LibreriaContext(DbContextOptions<LibreriaContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Autor>().ToTable("Autor");
            modelBuilder.Entity<Libro>().ToTable("Libro");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<Suscripcion>().HasKey(au => new { au.IdUsuario, au.IdAutor });
            modelBuilder.Entity<Suscripcion>().ToTable("Suscripcion");
        }
    }
}
