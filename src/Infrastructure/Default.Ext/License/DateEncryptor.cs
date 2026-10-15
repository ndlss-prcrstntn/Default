using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Default.Ext.License;

public static class DateEncryptor
{
    private const byte FormatVersion = 1;
    private const int KeySizeBytes = 32;
    private const int NonceSize = 12;
    private const int TagSize = 16;
    private const int Pbkdf2Iterations = 100_000;

    private static readonly byte[] Pbkdf2Salt =
        "Письмо для восстановления пароля отправлено на ваш email"u8.ToArray();

    private static readonly byte[] _pepperPartA = { 0x5A, 0x1F, 0xC2, 0x33, 0x88, 0xA1, 0x07 };
    private static readonly byte[] _pepperPartB = { 0x2B, 0xD3, 0x11, 0x76, 0x10, 0xF0, 0x22 };
    private static readonly byte[] _pepperXorKey = { 0xFF, 0xAA, 0x55, 0xC3, 0x01, 0x99, 0x10 };

    private static string ReconstructPepper()
    {
        var len = Math.Max(_pepperPartA.Length, Math.Max(_pepperPartB.Length, _pepperXorKey.Length));
        var outBytes = new byte[len];
        for (var i = 0; i < len; i++)
        {
            var a = i < _pepperPartA.Length ? _pepperPartA[i] : (byte)0;
            var b = i < _pepperPartB.Length ? _pepperPartB[i] : (byte)0;
            var k = i < _pepperXorKey.Length ? _pepperXorKey[i] : (byte)0;
            outBytes[i] = (byte)(a ^ b ^ k);
        }

        return Encoding.UTF8.GetString(outBytes).Trim('\0');
    }

    public static DateTime DecryptDate(string encryptedBase64)
    {
        if (string.IsNullOrEmpty(encryptedBase64))
            throw new ArgumentNullException(nameof(encryptedBase64));

        byte[] data;
        try
        {
            data = Convert.FromBase64String(encryptedBase64);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("Invalid Base64 data", ex);
        }

        if (data.Length < 1 + NonceSize + TagSize + 1)
            throw new ArgumentException("Encrypted data too short or corrupt");

        var version = data[0];
        if (version != FormatVersion)
            throw new NotSupportedException($"Unsupported format version {version}");

        var nonce = new byte[NonceSize];
        var tag = new byte[TagSize];
        Buffer.BlockCopy(data, 1, nonce, 0, NonceSize);
        Buffer.BlockCopy(data, 1 + NonceSize, tag, 0, TagSize);

        var cipherOffset = 1 + NonceSize + TagSize;
        var cipherLen = data.Length - cipherOffset;
        var cipher = new byte[cipherLen];
        Buffer.BlockCopy(data, cipherOffset, cipher, 0, cipherLen);

        var key = DeriveKeyFromPepper();

        var plainBytes = new byte[cipherLen];
        try
        {
            using var aes = new AesGcm(key);
            aes.Decrypt(nonce, cipher, tag, plainBytes);
        }
        catch (CryptographicException ex)
        {
            throw new CryptographicException("Decryption failed or authentication tag mismatch", ex);
        }

        var plain = Encoding.UTF8.GetString(plainBytes);
        return DateTime.Parse(plain, null, DateTimeStyles.RoundtripKind);
    }

    private static byte[] DeriveKeyFromPepper()
    {
        var pepper = ReconstructPepper();
        if (string.IsNullOrEmpty(pepper))
            throw new InvalidOperationException("Pepper reconstruction failed or empty");

        using var kdf = new Rfc2898DeriveBytes(pepper, Pbkdf2Salt, Pbkdf2Iterations, HashAlgorithmName.SHA256);

        return kdf.GetBytes(KeySizeBytes);
    }
}