using NutriStreak.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriStreak.Data;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL (
            @"server=localhost;
            database=NutriStreak;
            user=root;
            password=Senac2026;");
    }

}

