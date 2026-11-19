using NutriStreak.Models;

Console.WriteLine("NutriStreak");

await Usuario.Cadastrar("Teste", "teste@email.com", "hash-provisorio");

foreach (var u in await Usuario.ListarUsuarios())
{
    Console.WriteLine($"ID: {u.Id}, Nome: {u.Nome}, E-mail: {u.Email}");
}