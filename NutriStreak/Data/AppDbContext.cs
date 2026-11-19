using NutriStreak.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriStreak.Data;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL (
            @"server=127.0.0.1;
            database=NutriStreak;
            user=root;
            password=Senac2026;");
    }

}

