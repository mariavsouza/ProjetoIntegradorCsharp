namespace NutriStreak.Models;
public class Refeicao
{
    public int Id { get; set; } 

    public int UsuarioId { get; set; }

    public string? Alimentos { get; set; } 

    public string? Avaliacao { get; set; } = "";

    public DateTime DataRefeicao { get; set; } 

    public bool ValidarAlimentos()
    {
        return !string.IsNullOrWhiteSpace(Alimentos);
    }

    public void ExibirRefeicao()
    {
        Console.WriteLine("\n===== MINHA REFEIÇÃO =====");
        Console.WriteLine($"Alimentos: {Alimentos}");
        Console.WriteLine($"Avaliação: {Avaliacao}");
        Console.WriteLine($"Data: {DataRefeicao:dd/MM/yyyy HH:mm}");
    }
}