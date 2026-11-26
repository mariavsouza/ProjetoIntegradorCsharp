using Microsoft.EntityFrameworkCore;
using NutriStreak.Data;

namespace NutriStreak.Models;

public class Refeicao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public string Alimentos { get; set; } = string.Empty;
    public string Avaliacao { get; set; } = string.Empty;
    public DateTime DataRefeicao { get; set; }

    public static async Task<Refeicao> Cadastrar(
    int usuarioId,
    List<Alimento> escolhidos)
{
    await using var db = new AppDbContext();

    // Verifica se o usuário existe
    var usuarioExiste = await db.Usuarios
        .AnyAsync(u => u.Id == usuarioId);

    if (!usuarioExiste)
        throw new Exception("Usuário não encontrado!");

    if (escolhidos == null || escolhidos.Count == 0)
        throw new Exception("Selecione pelo menos um alimento!");

    var refeicao = new Refeicao
    {
        UsuarioId = usuarioId,
        Alimentos = string.Join(", ", escolhidos.Select(a => a.Nome)),
        Avaliacao = AvaliadorPrato.Avaliar(escolhidos),
        DataRefeicao = DateTime.Now
    };

    db.Refeicoes.Add(refeicao);
    await db.SaveChangesAsync();

    return refeicao;
}

    public static async Task<List<Refeicao>> Listar(int usuarioId)
    {
        await using var db = new AppDbContext();

        return await db.Refeicoes
            .AsNoTracking()
            .Where(r => r.UsuarioId == usuarioId)
            .OrderByDescending(r => r.DataRefeicao)
            .ToListAsync();
    }

    public static async Task<Refeicao?> BuscarPorId(
        int id, int usuarioId)
    {
        await using var db = new AppDbContext();

        return await db.Refeicoes.FirstOrDefaultAsync(
            r => r.Id == id && r.UsuarioId == usuarioId);
    }

    public static async Task<bool> Atualizar(
        int id,
        int usuarioId,
        List<Alimento> escolhidos)
    {
        await using var db = new AppDbContext();

        var refeicao = await db.Refeicoes.FirstOrDefaultAsync(
            r => r.Id == id && r.UsuarioId == usuarioId);

        if (refeicao == null || escolhidos.Count == 0)
            return false;

        refeicao.Alimentos = string.Join(
            ", ", escolhidos.Select(a => a.Nome));

        refeicao.Avaliacao = AvaliadorPrato.Avaliar(escolhidos);

        await db.SaveChangesAsync();

        return true;
    }

    public static async Task<bool> Excluir(
        int id, int usuarioId)
    {
        await using var db = new AppDbContext();

        var refeicao = await db.Refeicoes.FirstOrDefaultAsync(
            r => r.Id == id && r.UsuarioId == usuarioId);

        if (refeicao == null)
            return false;

        db.Refeicoes.Remove(refeicao);

        await db.SaveChangesAsync();

        return true;
    }

    public void ExibirRefeicao()
{
    Console.WriteLine("\n===== SUA REFEIÇÃO =====");
    Console.WriteLine($"Alimentos: {Alimentos}");
    Console.WriteLine($"Avaliação: {Avaliacao}");
    Console.WriteLine($"Data: {DataRefeicao}");
}
}