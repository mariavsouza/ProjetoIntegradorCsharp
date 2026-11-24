using Microsoft.EntityFrameworkCore;
using NutriStreak.Data;

namespace NutriStreak.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public int Pontos { get; set; }
    public int Sequencia { get; set; }
    public DateTime? UltimoRegistro { get; set; }
    public void RegistrarAtividade(int pontos)
    {
        Pontos += pontos;
        DateTime hoje = DateTime.Today;

        if (UltimoRegistro?.Date == hoje) return; 

        Sequencia = (UltimoRegistro?.Date == hoje.AddDays(-1)) ? Sequencia + 1 : 1;
        UltimoRegistro = hoje;
    }

    public static async Task<bool> Cadastrar(string nome, string email, string senhaHash)
    {
        await using var db = new AppDbContext();
        email = email.Trim().ToLower();

        if (await db.Usuarios.AnyAsync(u => u.Email == email))
        {
            Console.WriteLine("E-mail já cadastrado!");
            return false;
        }

        db.Usuarios.Add(new Usuario { Nome = nome, Email = email, SenhaHash = senhaHash });
        await db.SaveChangesAsync();
        return true;
    }

    public static async Task<Usuario?> BuscarPorEmail(string email)
    {
        await using var db = new AppDbContext();
        email = email.Trim().ToLower();
        return await db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public static async Task<Usuario?> BuscarPorId(int id)
    {
        await using var db = new AppDbContext();
        return await db.Usuarios.FindAsync(id);
    }
}