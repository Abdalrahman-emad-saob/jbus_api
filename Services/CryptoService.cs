using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using API.Interfaces;

namespace API.Services;


public class CryptoService : ICryptoService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public CryptoService(string key, string iv)
    {
        using var sha256 = SHA256.Create();
        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        _iv = Encoding.UTF8.GetBytes(iv[..16]);
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
       string encryptedText = Convert.ToBase64String(msEncrypt.ToArray());

    if (string.IsNullOrWhiteSpace(encryptedText) || encryptedText.Length % 4 != 0 || !Regex.IsMatch(encryptedText, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
    {
        throw new ArgumentException("The encryptedText is not a valid Base-64 string.", nameof(encryptedText));
    }
        return encryptedText;
    }

    public string Decrypt(string cipherText)
    {
        
        
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}
