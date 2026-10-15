using System.Security.Cryptography;

namespace Default.Ext;

public static class CryptoGenerator
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    /// <summary>
    ///     Генерирует хэш пароля.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>Хэшированный пароль в виде строки Base64.</returns>
    public static string GenerateHash(string password)
    {
        var salt = GenerateSalt();

        var hash = GenerateHash(password, salt);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    ///     Проверяет соответствие пароля и хэша.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="hashedPassword">Хэшированный пароль.</param>
    /// <returns>True, если пароль корректен, иначе False.</returns>
    public static bool VerifyHash(string password, string hashedPassword)
    {
        if (hashedPassword == null)
            return false;

        var hashBytes = Convert.FromBase64String(hashedPassword);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var hash = GenerateHash(password, salt);

        for (var i = 0; i < HashSize; i++)
            if (hashBytes[SaltSize + i] != hash[i])
                return false;

        return true;
    }

    /// <summary>
    ///     Генерирует случайную соль.
    /// </summary>
    /// <returns>Соль в виде массива байтов.</returns>
    private static byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(SaltSize);

    /// <summary>
    ///     Генерирует хэш пароля с использованием PBKDF2.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="salt">Соль для хэширования.</param>
    /// <returns>Хэш пароля в виде массива байтов.</returns>
    private static byte[] GenerateHash(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(HashSize);
    }
}