using Microsoft.EntityFrameworkCore;
using NutriStreak.Data;

namespace NutriStreak.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;

    public static async Task<bool> Cadastrar(string nome, string email, string senhaHash)
    {
        await using var db = new AppDbContext();

        if (await db.Usuarios.AnyAsync(u => u.Email == email))
        {
            Console.WriteLine("E-mail já cadastrado!");
            return false;
        }

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            SenhaHash = senhaHash
        };

        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();
        Console.WriteLine($"Usuário cadastrado! ID: {usuario.Id}");
        return true;
    }

    public static async Task<Usuario?> BuscarPorEmail(string email)
    {
        await using var db = new AppDbContext();
        return await db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public static async Task<List<Usuario>> ListarUsuarios()
    {
        await using var db = new AppDbContext();
        return await db.Usuarios.ToListAsync();
    }
}