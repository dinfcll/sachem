using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

public static class Crypto
{
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long. 
    //Une string de 16 charactères convertie en tableau de byte donne 32 bytes.
    private const string initVector = "retgaye1azpbzl28";
    // Constante déterminant la longueur de la clé d'encryption de l'algorithme.
    private const int keysize = 256;
    //Encrypt
    public static string Encrypt(string plainText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.Zeros;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }
    //Decrypt
    public static string Decrypt(string cipherText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.Zeros;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        //return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        return Encoding.ASCII.GetString(plainTextBytes).TrimEnd('\0');
    }
}