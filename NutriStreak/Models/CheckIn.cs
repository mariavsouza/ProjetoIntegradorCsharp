
namespace NutriStreak.Models;

public class CheckIn
{
    public int Id { get; set; } 

    public int UsuarioId { get; set; } 

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
}