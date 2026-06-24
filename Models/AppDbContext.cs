using Microsoft.EntityFrameworkCore;

namespace TiendaApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo estricto para la tabla usuarios (todo en minúsculas)
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasColumnName("password");

            // Mapeo estricto para la tabla productos (todo en minúsculas)
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Producto>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnName("precio");
            modelBuilder.Entity<Producto>().Property(p => p.ImagenUrl).HasColumnName("imagen_url");
        }
    }
}