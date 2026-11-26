using Microsoft.EntityFrameworkCore;
using NutriStreak.Data;
namespace NutriStreak.Models;

public class CheckIn
{
    public int Id { get; set; } 
    public int UsuarioId { get; set; } 
    public Usuario Usuario { get; set; } = new Usuario();
    public DateTime DataCheckIn { get; set; }
    public bool Agua { get; set; }
    public bool FrutasVerduras { get; set;} 
    public bool EvitouUltraprocessados { get; set; } 

    public int Pontos { get; set; }

    public int CalcularPontos()
    {
        int total = 0;

        if (Agua)
            total += 10;

        if (FrutasVerduras)
            total += 10;

        if (EvitouUltraprocessados) 
            total += 10;

        return total;
    }

    public void ExibirCheckIn()
    {
        Console.WriteLine($"\nData: {DataCheckIn:dd/MM/yyyy}");

        Console.WriteLine(
            $"Bebeu água: {(Agua ? "Sim" : "Não")}"
        );

        Console.WriteLine(
            $"Comeu frutas ou verduras: {(FrutasVerduras ? "Sim" : "Não")}"
        );

        Console.WriteLine(
            $"Evitu ultraprocessados: {(EvitouUltraprocessados ? "Sim" : "Não")}"
        );

        Console.WriteLine($"Pontos recebidos: {Pontos}");
    }
    public static async Task<bool> JaFezHoje(int usuarioId)
    {
        await using var db = new AppDbContext();
        DateTime hoje = DateTime.Today;
        return await db.CheckIns.AnyAsync(c => c.UsuarioId == usuarioId
            && c.DataCheckIn >= hoje && c.DataCheckIn < hoje.AddDays(1));
    }

    public static async Task<CheckIn?> RegistrarHoje(int usuarioId, bool agua, bool frutas, bool evitou)
    {
        if (await JaFezHoje(usuarioId)) return null;

        await using var db = new AppDbContext();
        var checkIn = new CheckIn
        {
            UsuarioId = usuarioId,
            DataCheckIn = DateTime.Now,
            Agua = agua,
            FrutasVerduras = frutas,
            EvitouUltraprocessados = evitou
        };
        checkIn.Pontos = checkIn.CalcularPontos();
        db.CheckIns.Add(checkIn);

        var usuario = await db.Usuarios.FindAsync(usuarioId);
        usuario!.RegistrarAtividade(checkIn.Pontos);

        await db.SaveChangesAsync();
        return checkIn;
    }
}