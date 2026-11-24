namespace NutriStreak.Models;

public record Alimento(string Nome, string Grupo);

public static class CatalogoAlimentos
{
    public static readonly List<Alimento> Todos = new()
    {
        new("Arroz", "Carboidrato"), new("Macarrão", "Carboidrato"),
        new("Batata", "Carboidrato"), new("Pão", "Carboidrato"),
        new("Feijão", "Leguminosa"), new("Lentilha", "Leguminosa"),
        new("Frango", "Proteína"), new("Carne", "Proteína"),
        new("Ovo", "Proteína"), new("Peixe", "Proteína"),
        new("Alface", "Verdura/Legume"), new("Tomate", "Verdura/Legume"),
        new("Cenoura", "Verdura/Legume"), new("Brócolis", "Verdura/Legume"),
        new("Banana", "Fruta"), new("Maçã", "Fruta"), new("Laranja", "Fruta"),
        new("Refrigerante", "Ultraprocessado"), new("Salgadinho", "Ultraprocessado"),
        new("Nuggets", "Ultraprocessado"), new("Biscoito recheado", "Ultraprocessado")
    };
}

public static class AvaliadorPrato
{
    public static string Avaliar(List<Alimento> escolhidos)
    {
        var grupos = escolhidos.Select(a => a.Grupo).Distinct().ToList();
        var dicas = new List<string>();

        if (!grupos.Contains("Verdura/Legume"))
            dicas.Add("Que tal incluir uma verdura ou legume? Eles trazem cor, fibras e vitaminas.");
        if (!grupos.Contains("Proteína") && !grupos.Contains("Leguminosa"))
            dicas.Add("Uma fonte de proteína, como ovo, frango ou feijão, ajuda a manter a energia nos estudos.");
        if (!grupos.Contains("Carboidrato"))
            dicas.Add("Arroz, batata ou pão ajudam a dar energia para o dia.");
        if (grupos.Contains("Ultraprocessado"))
            dicas.Add("Ultraprocessados podem aparecer de vez em quando; nas próximas refeições, vale equilibrar com alimentos naturais.");

        if (dicas.Count == 0)
            return $"Prato equilibrado e variado ({grupos.Count} grupos). Continue assim!";

        return "Bom registro! Ideias para as próximas refeições:\n- " + string.Join("\n- ", dicas);
    }
}
    