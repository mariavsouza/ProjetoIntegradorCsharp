
namespace NutriStreak.Models;

public class Planejamento
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = new Usuario();

    public DateTime DataRefeicao { get; set; }

    public string TipoRefeicao { get; set; } = "";

    public string Descricao { get; set; } = "";

    public bool ValidarPlanejamento()
    {
        return !string.IsNullOrWhiteSpace(TipoRefeicao)
                && !string.IsNullOrWhiteSpace(Descricao);
    }

    public void ExibirPlanejamento()
    {
        Console.WriteLine("\n==== REFEIÇÃO PLANEJADA =====");
        Console.WriteLine($"Data: {DataRefeicao:dd/MM/yyyy}");
        Console.WriteLine($"Tipo: {TipoRefeicao}");
        Console.WriteLine($"Descrição: {Descricao}");
    }
}