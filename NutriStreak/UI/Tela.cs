namespace NutriStreak.UI;

public static class Tela
{
    public static void Sucesso(string msg) => Escrever("✔ " + msg, ConsoleColor.Green);
    public static void Erro(string msg) => Escrever("✖ " + msg, ConsoleColor.Red);
    public static void Info(string msg) => Escrever(msg, ConsoleColor.Cyan);

    private static void Escrever(string msg, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public static void Pausa()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey(true);
    }

    public static string LerTexto(string pergunta)
    {
        Console.Write(pergunta);
        return (Console.ReadLine() ?? "").Trim();
    }

    public static bool LerSimNao(string pergunta)
    {
        while (true)
        {
            string r = LerTexto(pergunta + " (s/n): ").ToLower();
            if (r == "s") return true;
            if (r == "n") return false;
            Erro("Digite apenas s ou n.");
        }
    }

    public static string LerSenha(string pergunta)
    {
        Console.Write(pergunta);
        var senha = new System.Text.StringBuilder();
        while (true)
        {
            var tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.Enter) { Console.WriteLine(); break; }
            if (tecla.Key == ConsoleKey.Backspace)
            {
                if (senha.Length > 0) { senha.Length--; Console.Write("\b \b"); }
            }
            else if (!char.IsControl(tecla.KeyChar))
            {
                senha.Append(tecla.KeyChar);
                Console.Write('*');
            }
        }
        return senha.ToString();
    }
}