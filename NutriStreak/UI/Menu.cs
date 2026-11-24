
using NutriStreak.Models;

namespace NutriStreak.UI;

public static class Menu
{
    public static async Task Iniciar()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("===== NUTRISTREAK =====");
            Console.WriteLine("1 - ENTRAR");
            Console.WriteLine("2 - CADASTRAR USUÁRIO");
            Console.WriteLine("0 - SAIR");

            switch (Tela.LerTexto("Escolha uma opção: "))
            {
                case "1":
                    await Entrar();
                    break;

                case "2":
                    await Cadastrar();
                    break;

                case "0":
                    executando = false;
                    Console.WriteLine("Até logo!");
                    break;

                default:
                    Tela.Erro("Opção inválida!");
                    Tela.Pausa();
                    break;
            }
        }
    }

    private static async Task Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("===== CADASTRO =====");

        string nome = Tela.LerTexto("Nome: ");
        string email = Tela.LerTexto("E-mail: ").ToLower();
        string senha = Tela.LerSenha("Senha (mínimo 6 caracteres): ");

        if (nome == "" || email == "" || senha == "")
            Tela.Erro("Todos os campos são obrigatórios!");

        else if (!email.Contains('@') || !email.Contains('.'))
            Tela.Erro("E-mail inválido!");

        else if (senha.Length < 6)
            Tela.Erro("A senha precisa ter pelo menos 6 caracteres.");

        else if (await Usuario.Cadastrar(
            nome, email, SegurancaSenha.GerarHash(senha)))
            Tela.Sucesso("Cadastro realizado com sucesso!");

        Tela.Pausa();
    }

    private static async Task Entrar()
    {
        Console.Clear();
        Console.WriteLine("===== ENTRAR =====");

        string email = Tela.LerTexto("E-mail: ").ToLower();
        string senha = Tela.LerSenha("Senha: ");

        var usuario = await Usuario.BuscarPorEmail(email);

        if (usuario != null &&
            SegurancaSenha.VerificarSenha(senha, usuario.SenhaHash))
        {
            Tela.Sucesso($"Bem-vindo, {usuario.Nome}!");
            Tela.Pausa();

            await MenuUsuario(usuario.Id);
        }
        else
        {
            Tela.Erro("E-mail ou senha incorretos!");
            Tela.Pausa();
        }
    }

    private static async Task MenuUsuario(int usuarioId)
    {
        bool logado = true;

        while (logado)
        {
            var usuario = await Usuario.BuscarPorId(usuarioId);

            if (usuario == null)
                return;

            Console.Clear();

            Console.WriteLine($"===== OLÁ, {usuario.Nome.ToUpper()} =====");
            Console.WriteLine($"Pontos: {usuario.Pontos} | Sequência: {usuario.Sequencia} dia(s)\n");

            Console.WriteLine("1 - CHECK-IN DE HOJE");
            Console.WriteLine("2 - GERENCIAR PRATOS");
            Console.WriteLine("0 - SAIR DA CONTA");

            switch (Tela.LerTexto("Escolha uma opção: "))
            {
                case "1":
                    await FazerCheckIn(usuarioId);
                    break;

                case "2":
                    await GerenciarPratos(usuarioId);
                    break;

                case "0":
                    logado = false;
                    break;

                default:
                    Tela.Erro("Opção inválida!");
                    Tela.Pausa();
                    break;
            }
        }
    }

    private static async Task FazerCheckIn(int usuarioId)
    {
        Console.Clear();
        Console.WriteLine("===== CHECK-IN DE HOJE =====");

        if (await CheckIn.JaFezHoje(usuarioId))
        {
            Tela.Info("Você já fez o check-in de hoje. Volte amanhã para manter sua sequência!");
            Tela.Pausa();
            return;
        }

        bool agua = Tela.LerSimNao("Você bebeu água hoje?");
        bool frutas = Tela.LerSimNao("Comeu frutas ou verduras?");
        bool evitou = Tela.LerSimNao("Evitou ultraprocessados?");

        var checkIn = await CheckIn.RegistrarHoje(
            usuarioId, agua, frutas, evitou);

        if (checkIn != null)
        {
            Tela.Sucesso($"Check-in concluído! +{checkIn.Pontos} pontos");
            checkIn.ExibirCheckIn();
        }

        Tela.Pausa();
    }

    private static async Task GerenciarPratos(int usuarioId)
    {
        bool executando = true;

        while (executando)
        {
            Console.Clear();

            Console.WriteLine("===== GERENCIAR PRATOS =====");
            Console.WriteLine("1 - CADASTRAR PRATO");
            Console.WriteLine("2 - LISTAR PRATOS");
            Console.WriteLine("3 - ATUALIZAR PRATO");
            Console.WriteLine("4 - EXCLUIR PRATO");
            Console.WriteLine("0 - VOLTAR");

            switch (Tela.LerTexto("\nEscolha uma opção: "))
            {
                case "1":
                    await CadastrarPrato(usuarioId);
                    break;

                case "2":
                    await ListarPratos(usuarioId);
                    break;

                case "3":
                    await AtualizarPrato(usuarioId);
                    break;

                case "4":
                    await ExcluirPrato(usuarioId);
                    break;

                case "0":
                    executando = false;
                    break;

                default:
                    Tela.Erro("Opção inválida!");
                    Tela.Pausa();
                    break;
            }
        }
    }

    private static async Task CadastrarPrato(int usuarioId)
    {
        Console.Clear();
        Console.WriteLine("===== CADASTRAR PRATO =====");

        var escolhidos = SelecionarAlimentos();

        if (escolhidos.Count == 0)
        {
            Tela.Erro("Nenhum alimento válido foi escolhido.");
            Tela.Pausa();
            return;
        }

        var refeicao = await Refeicao.Cadastrar(usuarioId, escolhidos);

        Tela.Sucesso("Prato cadastrado com sucesso!");
        refeicao.ExibirRefeicao();

        Tela.Pausa();
    }

    private static async Task ListarPratos(int usuarioId)
    {
        Console.Clear();
        Console.WriteLine("===== MEUS PRATOS =====");

        var pratos = await Refeicao.Listar(usuarioId);

        if (pratos.Count == 0)
        {
            Tela.Info("Você ainda não possui pratos cadastrados.");
        }
        else
        {
            foreach (var prato in pratos)
            {
                Console.WriteLine("\n-------------------------");
                Console.WriteLine($"ID: {prato.Id}");
                Console.WriteLine($"Alimentos: {prato.Alimentos}");
                Console.WriteLine($"Avaliação: {prato.Avaliacao}");
                Console.WriteLine($"Data: {prato.DataRefeicao}");
            }

            Console.WriteLine("-------------------------");
        }

        Tela.Pausa();
    }

    private static async Task AtualizarPrato(int usuarioId)
    {
        Console.Clear();
        Console.WriteLine("===== ATUALIZAR PRATO =====");

        var pratos = await Refeicao.Listar(usuarioId);

        if (pratos.Count == 0)
        {
            Tela.Info("Não existem pratos cadastrados.");
            Tela.Pausa();
            return;
        }

        foreach (var prato in pratos)
        {
            Console.WriteLine($"ID: {prato.Id} - {prato.Alimentos}");
        }

        if (!int.TryParse(
            Tela.LerTexto("\nDigite o ID do prato: "), out int id))
        {
            Tela.Erro("ID inválido!");
            Tela.Pausa();
            return;
        }

        var existente = await Refeicao.BuscarPorId(id, usuarioId);

        if (existente == null)
        {
            Tela.Erro("Prato não encontrado!");
            Tela.Pausa();
            return;
        }

        Console.WriteLine("\nSelecione os novos alimentos:");

        var escolhidos = SelecionarAlimentos();

        if (escolhidos.Count == 0)
        {
            Tela.Erro("Nenhum alimento válido foi escolhido.");
            Tela.Pausa();
            return;
        }

        bool atualizado = await Refeicao.Atualizar(
            id, usuarioId, escolhidos);

        if (atualizado)
            Tela.Sucesso("Prato atualizado com sucesso!");
        else
            Tela.Erro("Não foi possível atualizar o prato.");

        Tela.Pausa();
    }

    private static async Task ExcluirPrato(int usuarioId)
    {
        Console.Clear();
        Console.WriteLine("===== EXCLUIR PRATO =====");

        var pratos = await Refeicao.Listar(usuarioId);

        if (pratos.Count == 0)
        {
            Tela.Info("Não existem pratos cadastrados.");
            Tela.Pausa();
            return;
        }

        foreach (var prato in pratos)
        {
            Console.WriteLine($"ID: {prato.Id} - {prato.Alimentos}");
        }

        if (!int.TryParse(
            Tela.LerTexto("\nDigite o ID do prato: "), out int id))
        {
            Tela.Erro("ID inválido!");
            Tela.Pausa();
            return;
        }

        var existente = await Refeicao.BuscarPorId(id, usuarioId);

        if (existente == null)
        {
            Tela.Erro("Prato não encontrado!");
            Tela.Pausa();
            return;
        }

        Console.WriteLine($"\nPrato: {existente.Alimentos}");

        if (!Tela.LerSimNao("Deseja realmente excluir este prato?"))
        {
            Tela.Info("Exclusão cancelada.");
            Tela.Pausa();
            return;
        }

        bool excluido = await Refeicao.Excluir(id, usuarioId);

        if (excluido)
            Tela.Sucesso("Prato excluído com sucesso!");
        else
            Tela.Erro("Não foi possível excluir o prato.");

        Tela.Pausa();
    }

    private static List<Alimento> SelecionarAlimentos()
    {
        var lista = CatalogoAlimentos.Todos;

        Console.WriteLine("\n===== ALIMENTOS DISPONÍVEIS =====");

        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1,2} - {lista[i].Nome} ({lista[i].Grupo})");
        }

        string entrada = Tela.LerTexto(
            "\nDigite os números separados por vírgula (ex: 1,5,12): ");

        var escolhidos = new List<Alimento>();

        foreach (var parte in entrada.Split(
            ',',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries))
        {
            if (int.TryParse(parte, out int numero) &&
                numero >= 1 &&
                numero <= lista.Count)
            {
                var alimento = lista[numero - 1];

                if (!escolhidos.Contains(alimento))
                    escolhidos.Add(alimento);
            }
        }

        return escolhidos;
    }
}