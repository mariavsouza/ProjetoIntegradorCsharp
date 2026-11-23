using System.Security.Cryptography;


namespace NutriStreak.Models;

public static class SegurancaSenha
{
    public static string GerarHash(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );

        return Convert.ToBase64String(salt)
            + "$"
            + Convert.ToBase64String(hash);
    }

    public static bool VerificarSenha(string senha, string senhaHash)
    {
        try
        {
            string[] partes = senhaHash.Split('$');

            if (partes.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(partes[0]);

            byte[] hashOriginal = Convert.FromBase64String(partes[1]);

            byte[] hashDigitado = Rfc2898DeriveBytes.Pbkdf2(
                senha,
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32
            );

            return CryptographicOperations.FixedTimeEquals(
                hashOriginal,
                hashDigitado
            );
        }
        catch
        {
            return false;
        }
    }
}
