using NutriStreak.Models;

bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("===== NUTRISTREAK =====");
    Console.WriteLine("1 - ENTRAR");
    Console.WriteLine("2 - CADASTRAR USUÁRIO");
    Console.WriteLine("0 - SAIR");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Digite seu e-mail: ");
            string? email = Console.ReadLine();
            Console.Write("Digite sua senha: ");
            string? senha = Console.ReadLine();

            var usuario = await Usuario.BuscarPorEmail(email ?? "");

            if (usuario != null && usuario.SenhaHash == senha)
            {
                Console.WriteLine($"Bem-vindo, {usuario.Nome}!");
            }
            else
            {
                Console.WriteLine("E-mail ou senha incorretos!");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;

        case "2":
            Console.Write("Digite seu nome: ");
            string? nome = Console.ReadLine();
            Console.Write("Digite seu e-mail: ");
            string? novoEmail = Console.ReadLine();
            Console.Write("Digite sua senha: ");
            string? novaSenha = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(novoEmail) && !string.IsNullOrWhiteSpace(novaSenha))
            {
                bool cadastrado = await Usuario.Cadastrar(nome, novoEmail ?? "", novaSenha ?? "");
                if (cadastrado)
                {
                    Console.WriteLine("Cadastro realizado com sucesso!");
                }
            }
            else
            {
                Console.WriteLine("Todos os campos são obrigatórios!");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
            
        case "0":
            Console.WriteLine("Saindo...");
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
    }
}
