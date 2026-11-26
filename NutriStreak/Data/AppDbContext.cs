using NutriStreak.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriStreak.Data;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
    public DbSet<Refeicao> Refeicoes { get; set; }
    public DbSet<Planejamento> Planejamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(
            @"server=127.0.0.1;
            database=NutriStreak;
            user=root;
            password=Senac2026;");
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Usuario>()
        .Property(u => u.Email)
        .HasMaxLength(150)
        .IsRequired();

    modelBuilder.Entity<Usuario>()
        .HasIndex(u => u.Email)
        .IsUnique();

    modelBuilder.Entity<Refeicao>()
        .HasOne(r => r.Usuario)
        .WithMany()
        .HasForeignKey(r => r.UsuarioId);

    modelBuilder.Entity<CheckIn>()
        .HasOne(c => c.Usuario)
        .WithMany()
        .HasForeignKey(c => c.UsuarioId);

    modelBuilder.Entity<Planejamento>()
        .HasOne(p => p.Usuario)
        .WithMany()
        .HasForeignKey(p => p.UsuarioId);
}

}